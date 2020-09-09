namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (String.IsNullOrEmpty(parentheses) || parentheses.Length % 2 != 0)
            {
                return false;
            }

            Stack<Char> parenthesesStack = new Stack<char>();
            for (int i = 0; i < parentheses.Length; i++)
            {
                char currentSymbol = parentheses[i];

                if (currentSymbol == '{'
                    || currentSymbol == '('
                    || currentSymbol == '[')
                {
                    parenthesesStack.Push(currentSymbol);
                }
                else
                {
                    switch (parenthesesStack.Pop())
                    {
                        case '{': return currentSymbol == '}';
                        case '(': return currentSymbol == ')';
                        case '[': return currentSymbol == ']';
                    }
                }
            }

            return false;
        }
    }
}
