using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据结构
{
    public class Sort
    {
        /// <summary>
        /// 直接插入排序
        /// </summary>
        /// <param name="sqList"></param>
        public int[] InsertSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[i - 1])
                {
                    int temp = array[i];
                    int j = 0;
                    for (j = i - 1; j >= 0 && temp > array[j]; j--)
                    {
                        array[j + 1] = array[j];
                    }
                    array[j + 1] = temp;
                }
            }
            return array;
        }

        public int[] BubbleSort(int[] array, out int cnt)
        {
            int temp;
            cnt = 0;
            var change = true;
            for (int i = 0; i < array.Length && change; i++)
            {
                for (int j = array.Length - 1; j > i && change; j--)
                {
                    cnt++;
                    change = false;
                    if (array[j] < array[j - 1])
                    {
                        change = true;
                        temp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temp;
                    }
                }
            }
            return array;
        }

        public int[] SimpleSelectSort(int[] array)
        {
            int tmp = 0;
            int t = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        t = j;
                    }
                }
                tmp = array[i];
                array[i] = array[t];
                array[t] = tmp;
            }
            return array;
        }

        public void QuickSort(int[] sqList, int low, int high)
        {
            int i = low;
            int j = high;
            int tmp = sqList[low];
            while (low < high)
            {
                while ((low < high) && (sqList[high] >= tmp))
                {
                    --high;
                }
                sqList[low] = sqList[high];
                ++low;
                while ((low < high) && (sqList[low] <= tmp))
                {
                    ++low;
                }
                sqList[high] = sqList[low];
                --high;
            }
            sqList[low] = tmp;
            if (i < low - 1)
            {
                QuickSort(sqList, i, low - 1);
            }
            if (low + 1 < j)
            {
                QuickSort(sqList, low + 1, j);
            }
        }
    }
}

