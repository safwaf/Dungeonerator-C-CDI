using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;

namespace GDIgame
{
    public class BoardManager
    {
        Dungeon D = new Dungeon();
        static int spritesize = 32;
        Player player = new Player();
        public BoardManager()
        {
        }

        public void DrawProcess(Graphics g)
        {
            D.DrawDungeon(g);
            //eventually we will draw the HUD at this level too
        }

        public void Tick()
        {
            D.current.UpdateRoom();
        }

        public void UpKey()
        {
            D.player.MoveUp();
            Tick();
        }
        public void DownKey()
        {
            D.player.MoveDown();
            Tick();
        }
        public void LeftKey()
        {
            D.player.MoveLeft();
            Tick();
        }
        public void RightKey()
        {
            D.player.MoveRight();
            Tick();
        }

    }
}
