using System;

namespace 继承_里氏替换原则
{
    #region 概念
    //任何父类出现的地方，子类都可以替代
    //
    //语法表现：父类容器装子类对象
    #endregion

    #region 实现
    //class GameObject
    //{

    //}

    //class Player : GameObject
    //{
    //    public void PlayerAtk()
    //    {
    //        Console.WriteLine("atk");
    //    }
    //}

    //class Boss : GameObject
    //{

    //}
    #endregion

    #region 练习一
    //class Monster
    //{

    //}

    //class Boss : Monster
    //{
    //    public void BossAtk()
    //    {
    //        Console.WriteLine("Boss技能");
    //    }
    //}

    //class Goblin : Monster
    //{
    //    public void GoblinAtk()
    //    {
    //        Console.WriteLine("Goblin攻击");
    //    }
    //}
    #endregion

    #region 练习二
    class Player
    {
        private Weapon weapon = new Weapon();
        
        public Player()
        {
            weapon = new Knife();
        }

        public void CheckWeapon()
        {
            while (true)
            {
                Console.WriteLine("请选择武器：Q选择刀，W选择喷子，E选择MP5");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Q:
                        weapon = new Knife();
                        (weapon as Knife).CheckKnife();
                        break;
                    case ConsoleKey.W:
                        weapon = new Shotgun();
                        (weapon as Shotgun).CheckShotgun();
                        break;
                    case ConsoleKey.E:
                        weapon = new Mp5();
                        (weapon as Mp5).CheckMp5();
                        break;
                    default:
                        Console.WriteLine("请按下正确按键");
                        break;
                }
            }
        }
    }

    class Weapon
    {

    }

    class Knife : Weapon
    {
        public void CheckKnife()
        {
            Console.WriteLine("你拿的是刀");
        }
    }

    class Shotgun : Weapon
    {
        public void CheckShotgun()
        {
            Console.WriteLine("你拿的是喷子");
        }
    }

    class Mp5 : Weapon
    {
        public void CheckMp5()
        {
            Console.WriteLine("你拿的是mp5");
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            #region is和as
            ////用父类容器装载子类对象
            //GameObject player = new Player();

            //// is 和 as
            ////概念
            //// is 判断一个对象是否是指定类对象
            //// 返回值：bool 是为真，不是为假
            ////
            //// as 将一个对象转换为指定类对象
            //// 返回值：指定类型对象
            //// 成功返回执行类型对象，失败返回null
            ////
            ////语法
            //// 类对象 is 类名    该语句块会有一个bool返回值 true 和 false
            //// 类对象 as 类名    该语句块会有一个对象返回值 对象 和 null

            //if (player is Player)
            //{
            //    //Player p = player as Player;
            //    //p.PlayerAtk();

            //    (player as Player).PlayerAtk();
            //}
            #endregion

            #region 练习一
            //Random r = new Random();
            //int randomInt;
            //Monster[] monsters = new Monster[10];

            //for (int i = 0; i < monsters.Length; i++)
            //{
            //    randomInt = r.Next(1, 101);
            //    if (randomInt < 50)
            //    {
            //        monsters[i] = new Boss();
            //    }
            //    else
            //    {
            //        monsters[i] = new Goblin();
            //    }
            //}

            //for (int i = 0; i < monsters.Length; i++)
            //{
            //    if(monsters[i] is Boss)
            //    {
            //        (monsters[i] as Boss).BossAtk();
            //    }
            //    else
            //    {
            //        (monsters[i] as Goblin).GoblinAtk();
            //    }
            //}
            #endregion

            Player p = new Player();
            p.CheckWeapon();
        }
    }
}
