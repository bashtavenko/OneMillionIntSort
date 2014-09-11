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
    private string[] _inputString;

    [TestInitialize]
    public void Init()
    {
      _input = GenerateRandomArray(1000000);
      _inputString = GenerateRandomStringArray(1000000);
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
      RadixSortDemo.LsdSort(input, 3);
      CollectionAssert.AreEqual(input, output);
    }

    [TestMethod]
    public void LstIntSortSimple()
    {
      int[] input = new int[] { 2, 8, 3, 1 };
      int[] output = new int[] { 1, 2, 3, 8 };
      RadixSortDemo.LsdIntSort(input, 4);
      CollectionAssert.AreEqual(input, output);
    }

    [TestMethod]
    public void LstIntSort10()
    {
      int[] input = GenerateRandomArray(10);      
      RadixSortDemo.LsdIntSort(input, 32);
      //Assert.IsTrue(IsSorted(input));
    }

    [TestMethod]
    public void LstIntSortOneMillion()
    {     
      RadixSortDemo.LsdIntSort(_input, 32);
      Assert.IsTrue(IsSorted(_input));
    }

    [TestMethod]
    public void LstIntSortOneMillionArraySort()
    {      
      Array.Sort(_input);
      //Assert.IsTrue(IsSorted(_input));
    }

    [TestMethod]
    public void LstIntSortOneMillionAsStrings()
    {        
        RadixSortDemo.LsdSort(_inputString, 10);
        //Assert.IsTrue(IsSorted(input));
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

    private string[] GenerateRandomStringArray(int n)
    {
        string[] a = new string[n];
        var r = new Random();

        for (int i = 0; i < n; i++)
        {
            a[i] = r.Next().ToString("{0000000000}");
        }
        return a;
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
