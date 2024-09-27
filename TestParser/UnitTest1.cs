using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace Parser.Tests
{
  public class Tests
  {
    [SetUp]
    public void Setup()
    {
      // Method intentionally left empty.
    }

    [Test]
    public void TestAddition()
    {
      int result = Parser.Add(5, 10);
      Assert.That(result, Is.EqualTo(15));
    }

    [Test]
    public void TestAddition_NegativeNumbers()
    {
      int result = Parser.Add(-3, -7);
      Assert.That(result, Is.EqualTo(-10));
    }

    [Test]
    public void TestAddition_Zero()
    {
      int result = Parser.Add(0, 0);
      Assert.That(result, Is.EqualTo(0));
    }

    [TestCase("2+3", "good")]
    [TestCase("5*6", "good")]
    [TestCase("7-4", "good")]
    [TestCase("9/3", "good")]
    [TestCase("1+", "bad")]
    [TestCase("1+-", "bad")]
    [TestCase("2/*1+-", "bad")]
    [TestCase("*8", "bad")]
    [TestCase("5/", "bad")]
    [TestCase("1", "good")]
    [TestCase("1+(2-3)", "good")]
    [TestCase("(1+2)-3", "good")]
    [TestCase("((1+2)-3", "bad")]
    [TestCase("(1+2)-3)", "bad")]
    [TestCase("(2*(2/3))", "good")]
    [TestCase("(1+2)-(2*3)", "good")]
    [TestCase("(1+(2-3*(2+3)))-(2*(4-5)*3)", "good")]
    [TestCase("1+(2-3*(2+3))-(2*(4-5)*3)", "good")]
    [TestCase("((((1+(2-3*(2+3))-(2*(4-5)*3)))))", "good")]
    [TestCase("((((1+(2-3*(2+3))-(2*(4-5)*3))", "bad")]
    [TestCase("(((())))", "bad")]
    [TestCase("(1)", "good")]
    [TestCase("(((((((1)))))))", "good")]
    public void ParseExpr_ValidInput_ReturnsExpectedResult(string input, string expectedResult, bool isClosed = true, int startInd = 0)
    {
      string result = Parser.ParseExpr(input, ref isClosed, ref startInd);
      Assert.That(result, Is.EqualTo(expectedResult));
    }
  }
}