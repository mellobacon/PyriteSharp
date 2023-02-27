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

    private void Advance()
    {
        _position++;
        _current = _position >= _text.Length ? '\0' : _text[_position];
    }

    private void LexNumber()
    {
        string temp = "";
        while (char.IsDigit(_current) || _current is '.' or '_')
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
        temp = _currenttext.Replace("_", "");
        
        if (temp.Contains('.'))
        {
            if (double.TryParse(temp, out double dvalue))
            {
                _value = dvalue;
                _tokentype = TokenType.NUMBER;
            }
        }
        else if (int.TryParse(temp, out int ivalue))
        {
            _value = ivalue;
            _tokentype = TokenType.NUMBER;
        }
        else
        {
            _tokentype = TokenType.BAD_TOKEN;
        }
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
                _tokentype = TokenType.PLUS;
                _currenttext += _current;
                Advance();
                break;
            case '-':
                _tokentype = TokenType.MINUS;
                _currenttext += _current;
                Advance();
                break;
            case '/':
                _tokentype = TokenType.SLASH;
                _currenttext += _current;
                Advance();
                break;
            case '*':
                _tokentype = TokenType.STAR;
                _currenttext += _current;
                Advance();
                break;
            case '%':
                _tokentype = TokenType.MOD;
                _currenttext += _current;
                Advance();
                break;
            case '=':
                _tokentype = TokenType.EQUAL;
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