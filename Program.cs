using System;

namespace 冒泡排序
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 冒泡排序知识点
            //两两相邻
            //不停比较
            //不停交换
            //比较n轮
            #endregion

            #region 练习
            Random r = new Random();
            int[] arr = new int[20];
            int temp;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(0, 101);
                Console.WriteLine(arr[i]);
            }
            Console.WriteLine("*****************************************");
            for(int m = 0; m < arr.Length; m++)
            {
                for(int n = 0; n < arr.Length - 1 - m; n++)
                {
                    if (arr[n] > arr[n + 1])
                    {
                        temp = arr[n];
                        arr[n] = arr[n+1];
                        arr[n + 1] = temp;
                    }                    
                }
            }
            for (int i = 0; i < arr.Length; i++)
            {                
                Console.WriteLine(arr[i]);
            }
            #endregion
        }
    }
}
