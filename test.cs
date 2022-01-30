using System;
using System.Collections.Generic;
using System.Linq;

// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution {
    public static void QS(int[] a, int min, int max)
        {
            int lo = min, hi = max, pvt = a[(min + max) / 2], swp;

            while (lo <= hi) {
                while (a[lo] < pvt) lo++;
                while (a[hi] > pvt) hi--;
                if (lo > hi) break;

                swp = a[lo]; a[lo] = a[hi]; a[hi] = swp;
                lo++; hi--;
            }
            if (min < hi) QS(a, min, hi);
            if (lo < max) QS(a, lo, max);
        }
    public static int main(int[] A) {
        // write your code in C# 6.0 with .NET 4.5 (Mono)
        QS(A, 0, A.Length-1);
        foreach(var i in A){
            Console.Write($"{i} ");
        }
    Console.WriteLine();    
        for(int i=0; i<A.Length-2; i++)
        {
           if(A[i]>0 && A[i]!=A[i+1] && A[i+1]-A[i]>=2){
                return A[i]+1;
            }
        }
            
       return A[A.Length-1]+1;
        
    }
}
