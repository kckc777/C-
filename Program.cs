using System;

namespace 飞行棋
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 控制台初始化
            int w = 50;
            int h = 30;
            ConsoleInit(w, h);
            #endregion

            E_SceneType nowSceneType = E_SceneType.Begin;

            #region 游戏场景循环
            while (true)
            {
                switch (nowSceneType)
                {
                    case E_SceneType.Begin:                       
                        Console.Clear();

                        StartScene(w, h, ref nowSceneType);
                        break;
                    case E_SceneType.Game:
                        Console.Clear();

                        GameScene(w, h, ref nowSceneType);
                        break;
                    case E_SceneType.End:                        
                        Console.Clear();

                        EndScene(w, h, ref nowSceneType);
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }

        #region 控制台初始化函数
        static void ConsoleInit(int w, int h)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
        }
        #endregion

        #region 开始场景函数
        static void StartScene(int w, int h, ref E_SceneType nowSceneType)
        {
            Console.SetCursorPosition(w / 2 - 3, h / 5);
            Console.Write("飞行棋");

            //当前选项id
            int nowSelIndex = 0;
            bool isQuitBegin = false;

            while (!isQuitBegin)
            {
                Console.SetCursorPosition(w / 2 - 4, 10);
                Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("开始游戏");

                Console.SetCursorPosition(w / 2 - 4, 12);
                Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("结束游戏");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        --nowSelIndex;
                        if (nowSelIndex < 0)
                        {
                            nowSelIndex = 0;
                        }
                        break;
                    case ConsoleKey.S:
                        ++nowSelIndex;
                        if (nowSelIndex > 1)
                        {
                            nowSelIndex = 1;
                        }
                        break;
                    case ConsoleKey.J:
                        if (nowSelIndex == 0)
                        {
                            nowSceneType = E_SceneType.Game;
                            isQuitBegin = true;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
            }
        }
        #endregion

        #region 结束场景函数
        static void EndScene(int w, int h, ref E_SceneType nowSceneType)
        {
            Console.SetCursorPosition(w / 2 - 4, h / 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("游戏结束");

            //当前选项id
            int nowSelIndex = 0;
            bool isQuitBegin = false;

            while (!isQuitBegin)
            {
                Console.SetCursorPosition(w / 2 - 4, 10);
                Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("重新游戏");

                Console.SetCursorPosition(w / 2 - 4, 12);
                Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        --nowSelIndex;
                        if (nowSelIndex < 0)
                        {
                            nowSelIndex = 0;
                        }
                        break;
                    case ConsoleKey.S:
                        ++nowSelIndex;
                        if (nowSelIndex > 1)
                        {
                            nowSelIndex = 1;
                        }
                        break;
                    case ConsoleKey.J:
                        if (nowSelIndex == 0)
                        {
                            nowSceneType = E_SceneType.Begin;
                            isQuitBegin = true;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
            }
        }
        #endregion

        #region 游戏场景逻辑
        static void GameScene(int w, int h, ref E_SceneType nowSceneType)
        {
            DrawWall(w, h);

            Map map = new Map(14, 3, 80);
            map.Draw();

            Player player = new Player(0, E_PlayType.Player);
            Player computer = new Player(0, E_PlayType.Computer);
            DrawPlayer(player, computer, map);
            bool isGameOver = false;

            while (true)
            {
                Console.ReadKey(true);
                isGameOver = RandomMove(w, h, ref player, ref computer, map);
                map.Draw();
                DrawPlayer(player, computer, map);
                if (isGameOver)
                {
                    Console.ReadKey(true);
                    nowSceneType = E_SceneType.End;
                    break;
                }

                Console.ReadKey(true);
                isGameOver = RandomMove(w, h, ref computer, ref player, map);
                map.Draw();
                DrawPlayer(player, computer, map);
                if (isGameOver)
                {
                    Console.ReadKey(true);
                    nowSceneType = E_SceneType.End;
                    break;
                }
            }
        }
        #endregion

        #region 红墙函数
        static void DrawWall(int w, int h)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            //横着的红墙
            for (int i = 0; i < w; i += 2)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("█");
                Console.SetCursorPosition(i, h - 1);
                Console.Write("█");
                Console.SetCursorPosition(i, h - 6);
                Console.Write("█");
                Console.SetCursorPosition(i, h - 11);
                Console.Write("█");
            }

            //竖着的红墙
            for (int i = 0; i < h; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("█");
                Console.SetCursorPosition(w - 2, i);
                Console.Write("█");
            }

            //文字信息
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 10);
            Console.Write("□：普通格子");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(2, h - 9);
            Console.Write("||：暂停，一回合不动");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(26, h - 9);
            Console.Write("●：炸弹，倒退5格");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(2, h - 8);
            Console.Write("？：时空隧道，随即倒退，暂停，换位置");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(2, h - 7);
            Console.Write("◇：玩家");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(12, h - 7);
            Console.Write("▽：电脑");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(22, h - 7);
            Console.Write("◎：玩家电脑重合");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 5);
            Console.Write("按任意键开始扔色子");
        }
        #endregion

        #region 绘制玩家函数
        static void DrawPlayer(Player player, Player computer, Map map)
        {
            if (player.nowIndex == computer.nowIndex)
            {
                Grid grid = map.grids[player.nowIndex];
                Console.SetCursorPosition(grid.pos.gridX, grid.pos.gridY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("◎");
            }
            else
            {
                player.Draw(map);
                computer.Draw(map);
            }
        }
        #endregion

        #region 扔色子函数
        static void ClearInfo(int h)
        {
            Console.SetCursorPosition(2, h - 5);
            Console.Write("                                              ");
            Console.SetCursorPosition(2, h - 4);
            Console.Write("                                              ");
            Console.SetCursorPosition(2, h - 3);
            Console.Write("                                              ");
            Console.SetCursorPosition(2, h - 2);
            Console.Write("                                              ");
        }

        static bool RandomMove(int w, int h, ref Player p, ref Player other, Map map)
        {
            ClearInfo(h);
            Console.ForegroundColor = p.type == E_PlayType.Player ? ConsoleColor.Cyan : ConsoleColor.Magenta;

            if (p.isPause)
            {
                Console.SetCursorPosition(2, h - 5);
                Console.Write("处于暂停状态，{0}需要暂停一回合", p.type == E_PlayType.Player ? "你" : "电脑");

                p.isPause = false;
                return false;
            }

            Random r = new Random();
            int randomNum = r.Next(1, 7);
            p.nowIndex += randomNum;

            Console.SetCursorPosition(2, h - 5);
            Console.Write("{0}扔出的点数为：{1}", p.type == E_PlayType.Player ? "你" : "电脑", randomNum);

            if (p.nowIndex >= map.grids.Length - 1)
            {
                p.nowIndex = map.grids.Length - 1;

                Console.SetCursorPosition(2, h - 4);
                if (p.type == E_PlayType.Player)
                {                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("恭喜你获胜，按任意键继续");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("遗憾，电脑获胜，按任意键继续");
                }
                Console.SetCursorPosition(2, h - 3);
                Console.Write("按任意键继续");
                return true;
            }
            else
            {
                Grid grid = map.grids[p.nowIndex];

                switch (grid.gridType)
                {
                    case E_Grid_Type.NormalGrid:
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}到达了一个安全位置", p.type == E_PlayType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键，让{0}开始扔色子", p.type == E_PlayType.Player ? "电脑" : "你");
                        break;
                    case E_Grid_Type.BoomGrid:
                        p.nowIndex -= 5;
                        if (p.nowIndex < 0)
                        {
                            p.nowIndex = 0;
                        }
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}踩到了炸弹，退后5格", p.type == E_PlayType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键，让{0}开始扔色子", p.type == E_PlayType.Player ? "电脑" : "你");
                        break;
                    case E_Grid_Type.PauseGrid:
                        p.isPause = true;
                        break;
                    case E_Grid_Type.TunnelGrid:
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}踩到了时空隧道", p.type == E_PlayType.Player ? "你" : "电脑");                       

                        randomNum = r.Next(1, 91);
                        if (randomNum < 30)
                        {
                            p.nowIndex -= 5;
                            if (p.nowIndex < 0)
                            {
                                p.nowIndex = 0;
                            }

                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("触发倒退5格");
                        }
                        else if (randomNum >= 30 && randomNum < 60)
                        {
                            p.isPause = true;

                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("触发暂停一回合");
                        }
                        else
                        {
                            int temp = p.nowIndex;
                            p.nowIndex = other.nowIndex;
                            other.nowIndex = temp;

                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("惊喜，双方交换位置");
                        }

                        Console.SetCursorPosition(2, h - 2);
                        Console.Write("请按任意键，让{0}开始扔色子", p.type == E_PlayType.Player ? "电脑" : "你");
                        break;
                }
            }

            return false;
        }
        #endregion
    }

    #region 游戏场景枚举
    enum E_SceneType
    {
        Begin,

        Game,

        End,
    }
    #endregion

    #region 格子结构体和格子枚举
    enum E_Grid_Type
    {
        NormalGrid,
        BoomGrid,
        PauseGrid,
        TunnelGrid,
    }

    //位置结构体
    struct Vector2
    {
        public int gridX;
        public int gridY;

        public Vector2(int gridX, int gridY)
        {
            this.gridX = gridX;
            this.gridY = gridY;
        }


    }

    struct Grid
    {
        //格子类型
        public E_Grid_Type gridType;
        //格子位置
        public Vector2 pos;

        public Grid(int gridX,int gridY,E_Grid_Type gridType)
        {
            pos.gridX = gridX;
            pos.gridY = gridY;
            this.gridType = gridType;
        }

        public void Draw()
        {
            Console.SetCursorPosition(pos.gridX, pos.gridY);
            switch (gridType)
            {
                case E_Grid_Type.NormalGrid:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("□");
                    break;
                case E_Grid_Type.BoomGrid:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("●");
                    break;
                case E_Grid_Type.PauseGrid:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("||");
                    break;
                case E_Grid_Type.TunnelGrid:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("？");
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    #region 地图结构体
    struct Map
    {
        public Grid[] grids;

        public Map(int x, int y, int num)
        {
            grids = new Grid[num];

            int indexX = 0;
            int indexY = 0;
            int stepNum = 2;

            Random r = new Random();
            int randomNum;
            for (int i = 0; i < num; i++)
            {
                randomNum = r.Next(0, 101);
                if (randomNum < 85 || i == 0 || i == num - 1)
                {
                    grids[i].gridType = E_Grid_Type.NormalGrid;
                }else if (randomNum >= 85 && randomNum < 90)
                {
                    grids[i].gridType = E_Grid_Type.BoomGrid;
                }else if (randomNum >= 90 && randomNum < 95)
                {
                    grids[i].gridType = E_Grid_Type.PauseGrid;
                }
                else
                {
                    grids[i].gridType = E_Grid_Type.TunnelGrid;
                }

                grids[i].pos = new Vector2(x, y);

                if (indexX == 10)
                {
                    y += 1;
                    ++indexY;
                    if (indexY == 2)
                    {
                        indexX = 0;
                        indexY = 0;
                        stepNum = -stepNum;
                    }
                }
                else
                {
                    x += stepNum;
                    ++indexX;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i].Draw();
            }
        }
    }
    #endregion

    #region 玩家枚举和玩家结构体
    enum E_PlayType
    {
        Player,
        Computer,
    }

    struct Player
    {
        public E_PlayType type;
        public int nowIndex;
        public bool isPause;

        public Player(int index,E_PlayType type)
        {
            nowIndex = index;
            this.type = type;
            isPause = false;
        }

        public void Draw(Map mapInfo)
        {
            Grid grid = mapInfo.grids[nowIndex];
            Console.SetCursorPosition(grid.pos.gridX, grid.pos.gridY);

            switch (type)
            {
                case E_PlayType.Player:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("◇");
                    break;
                case E_PlayType.Computer:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("▽");
                    break;
                default:
                    break;
            }
        }
    }
    #endregion
}
