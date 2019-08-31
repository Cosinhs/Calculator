using System;
using System.Collections.Generic;
namespace CalculatorCLI.Parser
{
    using Token;
    using AST;
    using Lexer;
    public class Parser
    {
        private Token _currentToken;
        private Token _peekToken;

        private Lexer _lexer;

        private static IDictionary<TokenType, Precedence> _precedences = new Dictionary<TokenType, Precedence>
        {
            {TokenType.PLUS, Precedence.SUM},
            {TokenType.MINUS, Precedence.SUM},
            {TokenType.MULTIPLY, Precedence.PRODUCT},
            {TokenType.DIVIDE, Precedence.PRODUCT},
            {TokenType.POWER, Precedence.POWER},
            {TokenType.LPAREN, Precedence.GROUP}
        };

        private IDictionary<TokenType, Func<Expression>> PrefixFns;

        private IDictionary<TokenType, Func<Expression, Expression>> InfixFns;


        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            ReadNextToken();
            ReadNextToken();
            PrefixFns = new Dictionary<TokenType, Func<Expression>>();
            InfixFns = new Dictionary<TokenType, Func<Expression, Expression>>();
            RegisterPrefix(TokenType.INT, ParseIntegerLiteral);
            RegisterPrefix(TokenType.MINUS, ParsePrefixExpression);
            RegisterPrefix(TokenType.LPAREN, ParseGroupExpression);
            RegisterInfix(TokenType.PLUS, ParseInfixExpression);
            RegisterInfix(TokenType.MINUS, ParseInfixExpression);
            RegisterInfix(TokenType.MULTIPLY, ParseInfixExpression);
            RegisterInfix(TokenType.DIVIDE, ParseInfixExpression);
            RegisterInfix(TokenType.POWER, ParseInfixExpression);

        }

        public Expression Parse()
        {
            return ParseExpression(Precedence.LOWEST);
        }
        
        private Expression ParseExpression(Precedence precedence)
        {
            if(PrefixFns.ContainsKey(_currentToken.Type))
            {
                Func<Expression> prefixFn = PrefixFns[_currentToken.Type];
                Expression leftExp = prefixFn();
                while(!PeekTokenIs(TokenType.EOF) && precedence < PeekTokenPrecedence())
                {
                    if(InfixFns.ContainsKey(_peekToken.Type))
                    {
                        Func<Expression, Expression> infixFn = InfixFns[_peekToken.Type];
                        ReadNextToken();
                        leftExp = infixFn(leftExp);
                    }
                    else
                    {
                        return leftExp;
                    }
                }
                return leftExp;
            }
            else
            {
                throw new Exception($"{_currentToken.Type} doesn't has corresponding parse function");
            }
        }

        private void RegisterPrefix(TokenType type, Func<Expression> fn)
        {
            PrefixFns.Add(type, fn);
        }

        private void RegisterInfix(TokenType type, Func<Expression, Expression> fn)
        {
            InfixFns.Add(type, fn);
        }

        private void ReadNextToken()
        {
            _currentToken = _peekToken;
            _peekToken = _lexer.NextToken();
        }

        private Expression ParseIntegerLiteral()
        {
            return new IntegerExpression
            {
                Token = _currentToken,
                Value = long.Parse(_currentToken.Literal)
            };
        }

        private Expression ParsePrefixExpression()
        {
            PrefixExpression exp = new PrefixExpression();
            exp.Token = _currentToken;
            exp.Operator = _currentToken.Literal;
            ReadNextToken();
            exp.Right = ParseExpression(Precedence.PREFIX);
            return exp;
        }

        private Expression ParseGroupExpression()
        {
            ReadNextToken();
            Expression exp = ParseExpression(Precedence.LOWEST);
            ExpectPeekToken(TokenType.RPAREN);
            return exp;
        }

        private Expression ParseInfixExpression(Expression left)
        {
            InfixExpression exp = new InfixExpression
            {
                Left = left,
                Token = _currentToken,
                Operator = _currentToken.Literal,
            };
            Precedence precedence = _precedences[_currentToken.Type];
            ReadNextToken();
            exp.Right = ParseExpression(precedence);
            return exp;
        }


        private bool PeekTokenIs(TokenType expectedToken)
        {
            return _peekToken.Type == expectedToken;
        }

        private bool CurrentTokenIs(TokenType expectedToken)
        {
            return _currentToken.Type == expectedToken;
        }

        private Precedence CurrentTokenPrecedence()
        {
            if(_precedences.ContainsKey(_currentToken.Type))
            {
                return _precedences[_currentToken.Type];
            }
            return Precedence.LOWEST;
            
        }

        private Precedence PeekTokenPrecedence()
        {
            if(_precedences.ContainsKey(_peekToken.Type))
            {
                return _precedences[_peekToken.Type];
            }
            return Precedence.LOWEST;
        }

        private void ExpectPeekToken(TokenType expectedToken)
        {
            if(PeekTokenIs(expectedToken))
            {
                ReadNextToken();
            }
            else
            {
                throw new Exception($"Expect token {expectedToken} but got {_peekToken.Type}");
            }
        }
    }
}
