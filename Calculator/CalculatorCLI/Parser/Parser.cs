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
            this._lexer = lexer;
            ReadNextToken();
            ReadNextToken();
            PrefixFns = new Dictionary<TokenType, Func<Expression>>();
            InfixFns = new Dictionary<TokenType, Func<Expression, Expression>>();
            RegisterPrefix(TokenType.INT, this.ParseIntegerLiteral);
            RegisterPrefix(TokenType.MINUS, this.ParsePrefixExpression);
            RegisterPrefix(TokenType.LPAREN, this.ParseGroupExpression);
            RegisterInfix(TokenType.PLUS, this.ParseInfixExpression);
            RegisterInfix(TokenType.MINUS, this.ParseInfixExpression);
            RegisterInfix(TokenType.MULTIPLY, this.ParseInfixExpression);
            RegisterInfix(TokenType.DIVIDE, this.ParseInfixExpression);
            RegisterInfix(TokenType.POWER, this.ParseInfixExpression);

        }

        public Expression Parse()
        {
            return this.ParseExpression(Precedence.LOWEST);
        }
        
        private Expression ParseExpression(Precedence precedence)
        {
            if(this.PrefixFns.ContainsKey(this._currentToken.Type))
            {
                Func<Expression> prefixFn = this.PrefixFns[this._currentToken.Type];
                Expression leftExp = prefixFn();
                while(!this.PeekTokenIs(TokenType.EOF) && precedence < this.PeekTokenPrecedence())
                {
                    if(this.InfixFns.ContainsKey(this._peekToken.Type))
                    {
                        Func<Expression, Expression> infixFn = this.InfixFns[this._peekToken.Type];
                        this.ReadNextToken();
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
                throw new Exception($"{this._currentToken.Type} doesn't has corresponding parse function");
            }
        }

        private void RegisterPrefix(TokenType type, Func<Expression> fn)
        {
            this.PrefixFns.Add(type, fn);
        }

        private void RegisterInfix(TokenType type, Func<Expression, Expression> fn)
        {
            this.InfixFns.Add(type, fn);
        }

        private void ReadNextToken()
        {
            this._currentToken = this._peekToken;
            this._peekToken = this._lexer.NextToken();
        }

        private Expression ParseIntegerLiteral()
        {
            return new IntegerExpression
            {
                Token = this._currentToken,
                Value = long.Parse(this._currentToken.Literal)
            };
        }

        private Expression ParsePrefixExpression()
        {
            PrefixExpression exp = new PrefixExpression();
            exp.Token = this._currentToken;
            exp.Operator = this._currentToken.Literal;
            this.ReadNextToken();
            exp.Right = this.ParseExpression(Precedence.PREFIX);
            return exp;
        }

        private Expression ParseGroupExpression()
        {
            this.ReadNextToken();
            Expression exp = this.ParseExpression(Precedence.LOWEST);
            this.ExpectPeekToken(TokenType.RPAREN);
            return exp;
        }

        private Expression ParseInfixExpression(Expression left)
        {
            InfixExpression exp = new InfixExpression
            {
                Left = left,
                Token = this._currentToken,
                Operator = this._currentToken.Literal,
            };
            Precedence precedence = _precedences[this._currentToken.Type];
            this.ReadNextToken();
            exp.Right = this.ParseExpression(precedence);
            return exp;
        }


        private bool PeekTokenIs(TokenType expectedToken)
        {
            return this._peekToken.Type == expectedToken;
        }

        private bool CurrentTokenIs(TokenType expectedToken)
        {
            return this._currentToken.Type == expectedToken;
        }

        private Precedence CurrentTokenPrecedence()
        {
            if(_precedences.ContainsKey(this._currentToken.Type))
            {
                return _precedences[this._currentToken.Type];
            }
            return Precedence.LOWEST;
            
        }

        private Precedence PeekTokenPrecedence()
        {
            if(_precedences.ContainsKey(this._peekToken.Type))
            {
                return _precedences[this._peekToken.Type];
            }
            return Precedence.LOWEST;
        }

        private void ExpectPeekToken(TokenType expectedToken)
        {
            if(PeekTokenIs(expectedToken))
            {
                this.ReadNextToken();
            }
            else
            {
                throw new Exception($"Expect token {expectedToken} but got {this._peekToken.Type}");
            }
        }
    }
}
