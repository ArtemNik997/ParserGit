using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
  public static class Parser
  {
    public static int Add(int a, int b)
    {
      return a + b;
    }

    public static string ParseExpr(string expr, ref bool isClosed, ref int curInd)
    {
      string res = "bad";
      for (int i = curInd; i < expr.Length; i++)
      {
        char cur = expr[i];

        string curType = GetSymbolType(cur);

        switch (curType)
        {
          case "num":
            {
              if (i + 1 >= expr.Length)
              {
                res = "good";
              }
              else if (GetSymbolType(expr[i + 1]) == "sign" || GetSymbolType(expr[i + 1]) == "par_close")
              {
                res = "good";
              }
              else
              {
                res = "bad";
              }
              break;
            }

          case "sign":
            {
              if (i == 0 || i + 1 >= expr.Length)
              {
                return "bad";
              }
              else if (GetSymbolType(expr[i + 1]) == "num" || GetSymbolType(expr[i + 1]) == "par_open")
              {
                res = "good";
              }
              break;
            }
            
          case "par_open":
            {
              bool locIsClosed = false;
              int locCurInd = i+1;
              res = ParseExpr(expr, ref locIsClosed, ref locCurInd);
              if (res == "bad")
              {
                return "bad";
              }
              if (locIsClosed)
              {
                res = "good";
                i = locCurInd;
              }
              else
              {
                res = "bad";
              }
              break;
            }
          case "par_close":
            {
              if (isClosed)
              {
                res = "bad";
              }
              else
              {
                isClosed = true;
                curInd = i;
              }
              return res;
            }

          default:
            res = "bad";
            break;
        }
      }

      if (!isClosed)
      {
        res = "bad";
      }

      return res;
    }

    private static string GetSymbolType(char symbol)
    {
      List<char> nums = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
      List<char> signs = new List<char> { '+', '-', '*', '/' };

      string symbolType;
      if (nums.Contains(symbol))
      {
        symbolType = "num";
      }
      else if (signs.Contains(symbol))
      {
        symbolType = "sign";
      }
      else if (symbol == '(')
      {
        symbolType = "par_open";
      }
      else if (symbol == ')')
      {
        symbolType = "par_close";
      }
      else
      {
        symbolType = "error";
      }

      return symbolType;
    }
  }
}
