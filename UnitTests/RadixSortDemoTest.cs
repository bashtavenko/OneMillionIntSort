using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Code;


namespace UnitTests
{
  [TestClass]
  public class RadixSortDemoTest
  {
    private int[] _input;
    private string[] _inputStrings;
    private int _numberOfDigits;

    [TestInitialize]
    public void Init()
    {
      _numberOfDigits = ((long)Math.Pow(2, 32)).ToString().Length;
      _input = GenerateRandomArray(1000000);
      _inputStrings = ConvertToStringArray();
      
    }

    [TestMethod]
    public void KeyIndexCounting()
    {
      int[] input = FromCharToInt(new int[]{ 'd', 'a', 'c', 'f', 'f', 'b', 'd', 'b', 'f', 'b', 'e', 'a' });
      int[] output = FromCharToInt(new int[] { 'a', 'a', 'b', 'b', 'b', 'c', 'd', 'd', 'e', 'f', 'f', 'f' });
      int radix = 6;
      int[] result = RadixSortDemo.KeyIndexCountingSort (input, radix);
      CollectionAssert.AreEqual(result, output);
    }

    [TestMethod]
    public void LsdRadixSort()
    {
      string[] input = new string[] {"dab",
                                     "add", 
                                     "cab", 
                                     "fad", 
                                     "fee", 
                                     "bad", 
                                     "dad",
                                     "bee",
                                     "fed",
                                     "bed",
                                     "ebb",
                                     "ace" };
      string[] output = new string[] {"ace",
                                     "add", 
                                     "bad", 
                                     "bed", 
                                     "bee", 
                                     "cab", 
                                     "dab",
                                     "dad",
                                     "ebb",
                                     "fad",
                                     "fed",
                                     "fee" };
      RadixSortDemo.LsdStringSort(input, 3);
      CollectionAssert.AreEqual(input, output);
    }

    [TestMethod]
    public void LsdNumbersRadixSort()
    {
        string[] input = new string[] {"20",
                                     "11", 
                                     "92", 
                                     "09"};
        string[] output = new string[] {"09",
                                     "11", 
                                     "20", 
                                     "92" };
        RadixSortDemo.LsdStringNumberSort(input, 2);
        CollectionAssert.AreEqual(input, output);
    }

    [TestMethod]
    public void LsdtIntSortSimple()
    {
      int[] input = new int[] { 2, 8, 3, 1 };
      int[] output = new int[] { 1, 2, 3, 8 };
      RadixSortDemo.LsdIntSort(input, 4);
      CollectionAssert.AreEqual(input, output);
    }

    [TestMethod]
    public void LsdtIntSortSimple2()
    {
      int[] input = GenerateRandomArray(10);      
      RadixSortDemo.LsdIntSort(input, 32);
      Assert.IsTrue(IsSorted(input));
    }

    [TestMethod]
    public void LsdIntSortOneMillion()
    {     
      RadixSortDemo.LsdIntSort(_input, 32);
      Assert.IsTrue(IsSorted(_input));
    }

    [TestMethod]
    public void ArraySortOneMillion()
    {      
      Array.Sort(_input);
      Assert.IsTrue(IsSorted(_input));
    }

    [TestMethod]
    public void LsdtIntSortOneMillionAsStrings()
    {   
        RadixSortDemo.LsdStringNumberSort(_inputStrings, _numberOfDigits);
        ConvertToIntArray(_inputStrings);
        Assert.IsTrue(IsSorted(_input));
    }

    private static int[] FromCharToInt(int[] a)
    {
      return a.Select(s => s - 97).ToArray();
    }

    private int[] GenerateRandomArray(int n)
    {
      int[] a = new int[n];
      var r = new Random();

      for (int i = 0; i < n; i++)
      {
        a[i] = r.Next();
      }
      return a;
    }

    private string[] ConvertToStringArray()
    {
        int n = _input.Length;
        string[] a = new string[n];

        string mask = string.Format("{0}", new string('0', _numberOfDigits));
        for (int i = 0; i < n; i++)
        {            
            a[i] = _input[i].ToString(mask);
        }
        return a;
    }

    private void ConvertToIntArray(string[] s)
    {
        int n = s.Length;        

        for (int i = 0; i < n; i++)
        {
            _input[i] = Convert.ToInt32(s[i]);
        }        
    }



    private bool IsSorted(int[] a)
    {
      for (int i = 1; i < a.Length; i++)
      {
        if (Less(a[i], a[i - 1])) return false;
      }
      return true;
    }

    private bool Less(int v, int w)
    {
      return v.CompareTo(w) < 0;
    }
  }
}
