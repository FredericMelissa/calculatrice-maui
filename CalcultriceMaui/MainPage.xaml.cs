using System.Data;
using System.Globalization;


namespace CalcultriceMaui;

public partial class MainPage : ContentPage
{
    private const double MaxCalculatorWidth = 420;

    // Chaine contenant toute l'expression saisie par l'utilisateur
    private string _expression = "";
    private bool _isEvaluated = false;

    public MainPage()
    {
        InitializeComponent();
        SizeChanged += OnPageSizeChanged;
    }

    private void OnPageSizeChanged(object? sender, EventArgs e)
    {
        if (Width <= 0) return;
        CalculatorRoot.WidthRequest = Math.Min(Width, MaxCalculatorWidth);
    }

    private void UpdateDisplay()
    {
        string displayText = string.IsNullOrEmpty(_expression) ? "0" : _expression;
        ResultLabel.Text = displayText;

        // Ajustement automatique de la taille du texte
        ResultLabel.FontSize = displayText.Length <= 10 ? 40 : displayText.Length <= 15 ? 30 : 22;
    }

    // Saisie des chiffres
    private void OnDigitClicked(object? sender, EventArgs e)
    {
        if (_isEvaluated)
        {
            _expression = "";
            _isEvaluated = false;
        }

        string digit = ((Button)sender!).Text;
        _expression += digit;
        UpdateDisplay();
    }

    // Saisie des parenthèses
    private void OnSymbolClicked(object? sender, EventArgs e)
    {
        if (_isEvaluated)
        {
            _isEvaluated = false;
        }

        string symbol = ((Button)sender!).Text;
        _expression += symbol;
        UpdateDisplay();
    }

    // Saisie du point décimal
    private void OnDecimalClicked(object? sender, EventArgs e)
    {
        if (_isEvaluated)
        {
            _expression = "0";
            _isEvaluated = false;
        }

        if (string.IsNullOrEmpty(_expression) || IsLastCharacterOperator(_expression[^1]))
        {
            _expression += "0.";
        }
        else
        {
            _expression += ".";
        }

        UpdateDisplay();
    }

    // Saisie des opérateurs (+, −, ×, ÷)
    private void OnOperatorClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_expression) && ((Button)sender!).Text != "−") return;

        if (_isEvaluated)
        {
            _isEvaluated = false;
        }

        string op = ((Button)sender!).Text;

        // Évite de mettre deux opérateurs de suite
        if (_expression.Length > 0 && IsLastCharacterOperator(_expression[^1]))
        {
            _expression = _expression[..^1] + op;
        }
        else
        {
            _expression += op;
        }

        UpdateDisplay();
    }

    // Évaluation globale de l'expression lors du clic sur "="
    private void OnEqualsClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_expression)) return;

        OperationLabel.Text = _expression + " =";

        try
        {
            // Conversion des symboles de la calculatrice
            string sanitized = _expression
                .Replace("×", "*")
                .Replace("÷", "/")
                .Replace("−", "-");

            // Évaluation de l'expression
            DataTable table = new DataTable();
            object resultObj = table.Compute(sanitized, null);

            double result = Convert.ToDouble(
                resultObj,
                CultureInfo.InvariantCulture
            );

            // Vérification du résultat
            if (double.IsInfinity(result) || double.IsNaN(result))
            {
                ShowError("Division par zéro impossible");
                return;
            }

            _expression = result.ToString(
                "G10",
                CultureInfo.InvariantCulture
            );

            ResultLabel.Text = _expression;
            _isEvaluated = true;
        }
        catch (DivideByZeroException)
        {
            ShowError("Division par zéro impossible");
        }
        catch
        {
            ShowError("Erreur de syntaxe");
        }
    }

    

    private void ShowError(string message)
    {
        OperationLabel.Text = message;
        ResultLabel.Text = "Erreur";
        _expression = "";
        _isEvaluated = true;
    }

    // Effacement complet (C)
    private void OnClearClicked(object? sender, EventArgs e)
    {
        _expression = "";
        OperationLabel.Text = "";
        _isEvaluated = false;
        UpdateDisplay();
    }

    // Effacement d'un caractère (←)
    private void OnBackspaceClicked(object? sender, EventArgs e)
    {
        if (_isEvaluated)
        {
            OnClearClicked(sender, e);
            return;
        }

        if (_expression.Length > 0)
        {
            _expression = _expression[..^1];
            UpdateDisplay();
        }
    }

    // Inversion de signe (±)
    private void OnSignClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_expression)) return;

        if (_expression.StartsWith("-"))
            _expression = _expression[1..];
        else
            _expression = "-(" + _expression + ")";

        UpdateDisplay();
    }

    // Pourcentage (%)
    private void OnPercentClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_expression)) return;

        _expression += "/100";
        UpdateDisplay();
    }

    private static bool IsLastCharacterOperator(char c)
    {
        return c == '+' || c == '−' || c == '-' || c == '×' || c == '*' || c == '÷' || c == '/';
    }
}