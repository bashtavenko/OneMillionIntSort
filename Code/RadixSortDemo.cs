using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class RadixSortDemo
    {
      public static int[] KeyIndexCountingSort(int[] a, int r)
      {
        int n = a.Length;
        int[] count = new int[r + 1];
        int[] aux = new int[n];

        // 1. Compute frequencies counts
        for (int i = 0; i < n; i++)
        {
          count[a[i]+1]++;
        }

        // 2. Transform counts to indices
        for (int j = 0; j < r; j++)
        {
          count[j + 1] += count[j];
        }

        // 3. Distribute the data
        for (int i = 0; i < n; i++)
        {
          aux[count[a[i]]++] = a[i];
        }

        // 4. Copy back
        for (int i = 0; i < n; i++)
        {
          a[i] = aux[i];
        }

        return a;
      }

      public static void LsdSort(string[] a, int w)
      {
        const int radix = 256;
        int n = a.Length;
        string[] aux = new string[n];

        for (int d = w - 1; d >= 0; d--)
        {
          int[] count = new int[radix + 1];
          for (int i = 0; i < n; i++)
          {            
            count[a[i][d] + 1]++;
          }
          for (int r = 0; r < radix; r++)
          {
            count[r + 1] += count[r];
          }
          for (int i = 0; i < n; i++)
          {            
            aux[count[a[i][d]]++] = a[i];
          }
          for (int i = 0; i < n; i++)
          {
            a[i] = aux[i];
          }
        }
      }

      public static void LsdIntSort(int[] a, int w)
      {
        int bits = w / 4;
        int radix = (int)Math.Pow(2, bits);
        int mask = radix - 1;
        int n = a.Length;
        int[] aux = new int[n];
                
        int digit;
        for (int d = 0; d < w; d+=bits)
        {          
          int[] count = new int[radix + 1];
          for (int i = 0; i < n; i++)
          {
            digit = (a[i] >> d) & mask;
            count[digit + 1]++;
          }
          for (int r = 0; r < radix; r++)
          {
            count[r + 1] += count[r];
          }
          for (int i = 0; i < n; i++)
          {
            digit = (a[i] >> d) & mask;
            aux[count[digit]++] = a[i];
          }
          for (int i = 0; i < n; i++)
          {
            a[i] = aux[i];
          }
        }
      }
    }
}
