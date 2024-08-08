using ClickableTransparentOverlay;
using ImGuiNET;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Memory;

namespace AIM_CS1._60
{
    public class Renderer : Overlay
    {

        public bool aimbot = false;
        public bool AMMO = false;
        public Vector2 screenSize = new Vector2(1920, 1080);
        public Vector2 g = new Vector2(960,551);
        public bool Trigger = false;
        public bool wall = false;
        public Vector4 circleColor = new Vector4(1, 1, 1, 1);
        private ConcurrentQueue<Entity> entities = new ConcurrentQueue<Entity>();
        private Entity localPlayer = new Entity();
        private readonly object entityLock = new object();

        ImDrawListPtr drawLiist;
        protected override void Render()
        {
            ImGui.Begin("menu");

            ImGui.Checkbox("aimbot", ref aimbot);
            ImGui.Checkbox("Trigger", ref Trigger);
            ImGui.Checkbox("wall", ref wall);
            ImGui.Checkbox("AMMO", ref AMMO);

            if (ImGui.CollapsingHeader("Teamcolor"))
            ImGui.ColorPicker4("##circlecolor", ref circleColor);


            DrawOverlay();
            drawLiist = ImGui.GetWindowDrawList();

            ImDrawListPtr drawList = ImGui.GetWindowDrawList();

            drawList.AddLine(g, Vector2.Add(g, new Vector2(0, 1070)), ImGui.ColorConvertFloat4ToU32(circleColor));
            drawList.AddLine(g, Vector2.Add(g, new Vector2(960, 0)), ImGui.ColorConvertFloat4ToU32(circleColor));
            drawList.AddLine(g, Vector2.Add(g, new Vector2(-960, 0)), ImGui.ColorConvertFloat4ToU32(circleColor));
            drawList.AddLine(g, Vector2.Add(g, new Vector2(0, -1070)), ImGui.ColorConvertFloat4ToU32(circleColor));
            // drawList.AddCircle(new Vector2(screenSize.X / 2, screenSize.Y / 2),FOV, ImGui.ColorConvertFloat4ToU32(circleColor));


        }

       

        void DrawOverlay()
        {
            ImGui.SetNextWindowSize(screenSize);
            ImGui.SetNextWindowPos(new Vector2(0, 0));
            ImGui.Begin("overlay", ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoScrollWithMouse
                | ImGuiWindowFlags.NoBackground
                | ImGuiWindowFlags.NoBringToFrontOnFocus
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoInputs
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoScrollbar
                );

           

        }
    }
}
