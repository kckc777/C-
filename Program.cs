using System;

namespace 选择排序
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(0, 100);
                Console.Write(arr[i] + " ");
            }

            int index;
            int temp;
            for (int i = 0; i < arr.Length; i++)
            {
                index = 0;
                for (int j = 1; j < arr.Length - i; j++)
                {
                    if (arr[index] < arr[j])
                    {
                        index = j;
                    }
                }

                if (index != arr.Length - 1 - i)
                {
                    temp = arr[index];
                    arr[index] = arr[arr.Length - 1 - i];
                    arr[arr.Length - 1 - i] = temp;
                }
            }
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
    }
}
