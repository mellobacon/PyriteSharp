namespace PyriteSharp.source.Syntax.Lexer;

public class Lexer
{
    private readonly string _text;
    private int _position;
    private char _current;
    private TokenType _tokentype;
    private string _currenttext;
    private object? _value;

    public Lexer(string text)
    {
        _text = text;
        _current = _text[0];
        _currenttext = "";
    }

    private void Advance(int offset = 1)
    {
        _position += offset;
        _current = _position >= _text.Length ? '\0' : _text[_position];
    }

    private char Peek(int offset = 1)
    {
        int index = _position + offset;
        return index >= _text.Length ? '\0' : _text[index];
    }

    private bool AttemptNumberConvert(string text)
    {
        bool hasdecimal = text.Contains('.');
        bool hasprefix = text.StartsWith("0b");
        
        _value = text;
        switch (text[^1])
        {
            case 'f':
                if (float.TryParse(text.Remove(text.Length - 1), out float fvalue))
                {
                    _value = fvalue;
                    _tokentype = TokenType.NUMBER;
                    return true;
                }

                break;
            case 'd':
                if (double.TryParse(text.Remove(text.Length - 1), out double dvalue))
                {
                    _value = dvalue;
                    _tokentype = TokenType.NUMBER;
                    return true;
                }
                break;
            default:
                if (hasprefix)
                {
                    _value = Convert.ToInt32(text.Replace("0b", ""), 2);
                    _tokentype = TokenType.NUMBER;
                    return true;
                }
                if (int.TryParse(text, out int ivalue) && !hasdecimal)
                {
                    _value = ivalue;
                    _tokentype = TokenType.NUMBER;
                    return true;
                }
                if (double.TryParse(text, out double value))
                {
                    _value = value;
                    _tokentype = TokenType.NUMBER;
                    return true;
                }
                
                break;
        }

        return false;
    }

    private void LexNumber()
    {
        while (char.IsDigit(_current) || _current is '.' or '_' or 'f' or 'd' or 'b')
        {
            _currenttext += _current;
            Advance();
        }

        if (_currenttext[0] == '_' || _currenttext[^1] == '.' || _currenttext[^1] == '_')
        {
            _tokentype = TokenType.BAD_TOKEN;
            return;
        }

        // mmmm sugar
        string temp = _currenttext.Replace("_", "");
        
        if (!AttemptNumberConvert(temp))
        {
            _tokentype = TokenType.BAD_TOKEN;
        }
    }

    private void LexKeyword()
    {
        while (char.IsLetter(_current))
        {
            _currenttext += _current;
            Advance();
        }

        SyntaxInfo.TryParseKeyword(_currenttext, out TokenType keywordtype, out bool? keywordvalue);
        _tokentype = keywordtype;
        _value = keywordvalue;
    }

    private void LexWhiteSpace()
    {
        while (char.IsWhiteSpace(_current))
        {
            Advance();
        }

        _tokentype = TokenType.WHITESPACE;
    }

    public Token Lex()
    {
        _value = null;
        _tokentype = TokenType.BAD_TOKEN;
        _currenttext = "";

        switch (_current)
        {
            case '\0':
                _tokentype = TokenType.EOF_TOKEN;
                break;
            case '+':
                if (Peek() == '=')
                {
                    _tokentype = TokenType.PLUS_EQUAL;
                    _currenttext += $"{_current}{Peek()}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.PLUS;
                _currenttext += _current;
                Advance();
                break;
            case '-':
                if (Peek() == '=')
                {
                    _tokentype = TokenType.MINUS_EQUAL;
                    _currenttext += $"{_current}{Peek()}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.MINUS;
                _currenttext += _current;
                Advance();
                break;
            case '/':
                if (Peek() == '=')
                {
                    _tokentype = TokenType.SLASH_EQUAL;
                    _currenttext += $"{_current}{Peek()}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.SLASH;
                _currenttext += _current;
                Advance();
                break;
            case '*':
                if (Peek() == '=')
                {
                    _tokentype = TokenType.STAR_EQUAL;
                    _currenttext += $"{_current}{Peek()}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.STAR;
                _currenttext += _current;
                Advance();
                break;
            case '%':
                if (Peek() == '=')
                {
                    _tokentype = TokenType.MOD_EQUAL;
                    _currenttext += $"{_current}{Peek()}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.MOD;
                _currenttext += _current;
                Advance();
                break;
            case '=':
                if (Peek() == '=')
                {
                    _tokentype = TokenType.DOUBLE_EQUAL;
                    _currenttext += $"{_current}{_current}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.EQUAL;
                _currenttext += _current;
                Advance();
                break;
            case '!':
                if (Peek() == '=')
                {
                    _tokentype = TokenType.BANG_EQUAL;
                    _currenttext += $"{_current}{Peek()}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.BANG;
                _currenttext += _current;
                Advance();
                break;
            case '>':
                if (Peek() == '>')
                {
                    _tokentype = TokenType.DOUBLE_MORE_THAN;
                    _currenttext += $"{_current}{_current}";
                    Advance(2);
                    break;
                }
                if (Peek() == '=')
                {
                    _tokentype = TokenType.MORE_EQUAL;
                    _currenttext += $"{_current}{Peek()}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.MORE_THAN;
                _currenttext += _current;
                Advance();
                break;
            case '<':
                if (Peek() == '<')
                {
                    _tokentype = TokenType.DOUBLE_LESS_THAN;
                    _currenttext += $"{_current}{_current}";
                    Advance(2);
                    break;
                }

                if (Peek() == '=')
                {
                    _tokentype = TokenType.LESS_EQUAL;
                    _currenttext += $"{_current}{Peek()}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.LESS_THAN;
                _currenttext += _current;
                Advance();
                break;
            case '|':
                if (Peek() == '|')
                {
                    _tokentype = TokenType.DOUBLE_PIPE;
                    _currenttext += $"{_current}{_current}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.PIPE;
                _currenttext += _current;
                Advance();
                break;
            case '&':
                if (Peek() == '&')
                {
                    _tokentype = TokenType.DOUBLE_AND;
                    _currenttext += $"{_current}{_current}";
                    Advance(2);
                    break;
                }
                _tokentype = TokenType.AND;
                _currenttext += _current;
                Advance();
                break;
            case '^':
                _tokentype = TokenType.HAT;
                _currenttext += _current;
                Advance();
                break;
            case '(':
                _tokentype = TokenType.LEFT_PAREN;
                _currenttext += _current;
                Advance();
                break;
            case ')':
                _tokentype = TokenType.RIGHT_PAREN;
                _currenttext += _current;
                Advance();
                break;
            default:
                if (char.IsDigit(_current) || _current is '.' or '_')
                {
                    LexNumber();
                    break;
                }

                if (char.IsLetter(_current))
                {
                    LexKeyword();
                    break;
                }

                if (char.IsWhiteSpace(_current))
                {
                    LexWhiteSpace();
                    break;
                }

                _tokentype = TokenType.BAD_TOKEN;
                Advance();
                break;
        }
        
        return new Token(_currenttext, _tokentype, _value);
    }
}