using System;
using UnityEngine;

/// <summary>
/// A class that represents a color using a hexadecimal string.
/// Provides functionality to convert between HexColor and Unity's Color and Color32 structures.
/// </summary>
[Serializable]
public class HexColor
{
    [SerializeField] private string _hex;
    // Private fields to store color components
    private byte _r;
    private byte _g;
    private byte _b;
    private byte _a;

    /// <summary>
    /// Red component (0-255).
    /// </summary>
    public byte R => _r;

    /// <summary>
    /// Green component (0-255).
    /// </summary>
    public byte G => _g;

    /// <summary>
    /// Blue component (0-255).
    /// </summary>
    public byte B => _b;

    /// <summary>
    /// Alpha component (0-255).
    /// </summary>
    public byte A => _a;

    #region Constructors

    /// <summary>
    /// Default constructor initializes the color to white with full opacity.
    /// </summary>
    public HexColor()
    {
        _r = 255;
        _g = 255;
        _b = 255;
        _a = 255;
    }

    /// <summary>
    /// Constructs a HexColor from a hex string.
    /// Supports the following formats:
    /// - RRGGBB
    /// - #RRGGBB
    /// - AARRGGBB
    /// - #AARRGGBB
    /// </summary>
    /// <param name="hex">Hexadecimal color string.</param>
    public HexColor(string hex)
    {
        ParseHex(hex);
    }

    public HexColor(string hex, float opacity)
    {
        ParseHex(hex);
        _a = (byte)(opacity * 255);
    }

    /// <summary>
    /// Constructs a HexColor from a Unity Color.
    /// </summary>
    /// <param name="color">Unity Color.</param>
    public HexColor(Color color)
    {
        _r = (byte)(color.r * 255);
        _g = (byte)(color.g * 255);
        _b = (byte)(color.b * 255);
        _a = (byte)(color.a * 255);
    }

    /// <summary>
    /// Constructs a HexColor from a Unity Color32.
    /// </summary>
    /// <param name="color32">Unity Color32.</param>
    public HexColor(Color32 color32)
    {
        _r = color32.r;
        _g = color32.g;
        _b = color32.b;
        _a = color32.a;
    }

    #endregion

    #region Hex Parsing

    /// <summary>
    /// Parses a hexadecimal color string and sets the color components.
    /// </summary>
    /// <param name="hex">Hexadecimal color string.</param>
    private void ParseHex(string hex)
    {
        if (string.IsNullOrEmpty(hex))
        {
            SetColor(255, 255, 255, 255); // Default to white
            return;
        }

        hex = hex.TrimStart('#');

        if (hex.Length == 6)
        {
            // Format: RRGGBB
            _r = Convert.ToByte(hex.Substring(0, 2), 16);
            _g = Convert.ToByte(hex.Substring(2, 2), 16);
            _b = Convert.ToByte(hex.Substring(4, 2), 16);
            _a = 255; // Default alpha
        }
        else if (hex.Length == 8)
        {
            // Format: AARRGGBB
            _a = Convert.ToByte(hex.Substring(0, 2), 16);
            _r = Convert.ToByte(hex.Substring(2, 2), 16);
            _g = Convert.ToByte(hex.Substring(4, 2), 16);
            _b = Convert.ToByte(hex.Substring(6, 2), 16);
        }
        else
        {
            Debug.LogError($"Invalid hex string format: {hex}. Expected 6 or 8 characters.");
            SetColor(255, 255, 255, 255); // Default to white
        }
    }

    /// <summary>
    /// Sets the color components.
    /// </summary>
    /// <param name="red">Red component (0-255).</param>
    /// <param name="green">Green component (0-255).</param>
    /// <param name="blue">Blue component (0-255).</param>
    /// <param name="alpha">Alpha component (0-255).</param>
    private void SetColor(byte red, byte green, byte blue, byte alpha)
    {
        _r = red;
        _g = green;
        _b = blue;
        _a = alpha;
    }

    #endregion

    #region Conversion Methods

    /// <summary>
    /// Converts HexColor to Unity's Color32.
    /// </summary>
    /// <returns>Color32 representation.</returns>
    public Color32 ToColor32()
    {
        return new Color32(_r, _g, _b, _a);
    }

    /// <summary>
    /// Converts HexColor to Unity's Color.
    /// </summary>
    /// <returns>Color representation.</returns>
    public Color ToColor()
    {
        return new Color(_r / 255f, _g / 255f, _b / 255f, _a / 255f);
    }

    /// <summary>
    /// Sets the HexColor from a Unity Color32.
    /// </summary>
    /// <param name="color32">Color32 to set from.</param>
    public void SetFromColor32(Color32 color32)
    {
        _r = color32.r;
        _g = color32.g;
        _b = color32.b;
        _a = color32.a;
    }

    /// <summary>
    /// Sets the HexColor from a Unity Color.
    /// </summary>
    /// <param name="color">Color to set from.</param>
    public void SetFromColor(Color color)
    {
        _r = (byte)(color.r * 255);
        _g = (byte)(color.g * 255);
        _b = (byte)(color.b * 255);
        _a = (byte)(color.a * 255);
    }

    #endregion

    #region Overrides

    /// <summary>
    /// Returns the hexadecimal string representation of the color.
    /// Format: #RRGGBB or #AARRGGBB based on alpha value.
    /// </summary>
    /// <returns>Hexadecimal color string.</returns>
    public override string ToString()
    {
        if (_a != 255)
        {
            return $"#{_a:X2}{_r:X2}{_g:X2}{_b:X2}";
        }
        else
        {
            return $"#{_r:X2}{_g:X2}{_b:X2}";
        }
    }

    #endregion

    #region Implicit Conversions

    /// <summary>
    /// Implicit conversion from HexColor to Color32.
    /// </summary>
    /// <param name="hexColor">HexColor instance.</param>
    public static implicit operator Color32(HexColor hexColor)
    {
        return hexColor.ToColor32();
    }

    /// <summary>
    /// Implicit conversion from Color32 to HexColor.
    /// </summary>
    /// <param name="color32">Color32 instance.</param>
    public static implicit operator HexColor(Color32 color32)
    {
        return new HexColor(color32);
    }

    /// <summary>
    /// Implicit conversion from HexColor to Color.
    /// </summary>
    /// <param name="hexColor">HexColor instance.</param>
    public static implicit operator Color(HexColor hexColor)
    {
        return hexColor.ToColor();
    }

    /// <summary>
    /// Implicit conversion from Color to HexColor.
    /// </summary>
    /// <param name="color">Color instance.</param>
    public static implicit operator HexColor(Color color)
    {
        return new HexColor(color);
    }

    #endregion

    #region Static Helper Methods

    /// <summary>
    /// Creates a HexColor from a hexadecimal string.
    /// </summary>
    /// <param name="hex">Hexadecimal color string.</param>
    /// <returns>HexColor instance.</returns>
    public static HexColor FromHex(string hex)
    {
        return new HexColor(hex);
    }

    /// <summary>
    /// Creates a HexColor from individual RGBA byte values.
    /// </summary>
    /// <param name="r">Red component (0-255).</param>
    /// <param name="g">Green component (0-255).</param>
    /// <param name="b">Blue component (0-255).</param>
    /// <param name="a">Alpha component (0-255).</param>
    /// <returns>HexColor instance.</returns>
    public static HexColor FromRGBA(byte r, byte g, byte b, byte a = 255)
    {
        HexColor hexColor = new HexColor();
        hexColor.SetColor(r, g, b, a);
        return hexColor;
    }

    #endregion
}