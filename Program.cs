using Memory;
using AIM_CS1._60;
using Swed32;
using System.Numerics;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using static AIM_CS1._60.Entity;

Swed swed = new Swed("hl2");

IntPtr hw = swed.GetModuleBase("client.dll");
IntPtr client = swed.GetModuleBase("client.dll");

const int HOSTKEY = 0x02;

Renderer renderer = new Renderer();
renderer.Start().Wait();

Mem m = new Mem();

Entity localPlayer = new Entity();
List<Entity> entities = new List<Entity>();

while (true)
{
    IntPtr entityList = swed.ReadPointer(hw, Offsets.entityList);
    localPlayer.Address = swed.ReadPointer(entityList, Offsets.initialEntity);
    localPlayer.Position = swed.ReadVec(localPlayer.Address, Offsets.position);
    localPlayer.Team = swed.ReadInt(localPlayer.Address, Offsets.team);

    



    int ID = swed.ReadInt(client, Offsets.cross);

    if (ID == 2 &&  renderer.Trigger)
    {
        
        swed.WriteBytes(client, Offsets.attack, BitConverter.GetBytes(5));
        Thread.Sleep(50);
        swed.WriteBytes(client, Offsets.attack, BitConverter.GetBytes(4));
    }
    

    int wall = swed.ReadInt(hw, Offsets.wall);

    if (GetAsyncKeyState(HOSTKEY) < 0 && wall >0 && renderer.wall)
    {
        
        swed.WriteBytes(hw, Offsets.wall, BitConverter.GetBytes(132968)); 
        Thread.Sleep(0);
    }
    else

    swed.WriteBytes(hw, Offsets.wall, BitConverter.GetBytes(131944));

    for (int i = 0; i < 30; i++)
    {
        IntPtr currentEnt = swed.ReadPointer(entityList, Offsets.initialEntity + i * Offsets.step);

        if (currentEnt == IntPtr.Zero)
            continue;

        IntPtr healthComponet = swed.ReadPointer(currentEnt, 0x4);
        float health = swed.ReadFloat(healthComponet, Offsets.health);
        int team = swed.ReadInt(currentEnt, Offsets.team);

       

        if (health > 0 && health < 101 && team != localPlayer.Team)
        {
            Entity entity = new Entity();
            entity.Address = currentEnt;
            entity.Team = team;
            entity.Health = health;
            entity.Position = swed.ReadVec(currentEnt, Offsets.position);
            entity.Distance = Vector3.Distance(localPlayer.Position, entity.Position);

            entities.Add(entity);

            
        }
    }
    entities = entities.OrderBy(o => o.Distance).ToList();

   


    if (GetAsyncKeyState(HOSTKEY) < 0 && renderer.aimbot && entities.Count >0)
    {  
        Vector2 calculatedAngles = Calculate.CalculateAngles(localPlayer.Position, entities[0].Position);
        Vector3 newAngleVec3 = new Vector3(calculatedAngles.Y, calculatedAngles.X, 0.0f);
        swed.WriteVec(hw, Offsets.viewAngles, newAngleVec3);
    }
    Thread.Sleep(0);
   

    entities.Clear();





    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int vKey);



}