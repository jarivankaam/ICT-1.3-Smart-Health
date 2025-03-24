using UnityEngine;
using UnityEngine.UI;

public class CastColorSwitcher : MonoBehaviour
{
    public Image image;
    public GameObject colorBlue;
    public GameObject colorBlack;
    public GameObject colorPink;
    public GameObject colorWhite;
    public GameObject colorYellow;
    
    private int colorNumber = 1;
    private const int MAX_COLORS = 5;
    
    [System.Serializable]
    private class ColorOption
    {
        public GameObject colorObject;
        public Color imageColor;
    }
    
    private ColorOption[] colorOptions;
    
    private void Awake()
    {
        // Initialize the color options array
        InitializeColorOptions();
    }
    
    private void InitializeColorOptions()
    {
        colorOptions = new ColorOption[MAX_COLORS];
        
        // Define all color options with their respective image colors
        colorOptions[0] = new ColorOption { colorObject = colorBlue, imageColor = Color.white };
        colorOptions[1] = new ColorOption { colorObject = colorBlack, imageColor = Color.white };
        colorOptions[2] = new ColorOption { colorObject = colorPink, imageColor = Color.white };
        colorOptions[3] = new ColorOption { colorObject = colorWhite, imageColor = Color.white };
        colorOptions[4] = new ColorOption { colorObject = colorYellow, imageColor = Color.white };
    }
    
    public void CheckColor()
    {
        SetColor(colorNumber);
    }
    
    public void NextColor()
    {
        colorNumber++;
        if (colorNumber > MAX_COLORS)
        {
            colorNumber = 1;
        }
        CheckColor();
    }
    
    public void PreviousColor()
    {
        colorNumber--;
        if (colorNumber < 1)
        {
            colorNumber = MAX_COLORS;
        }
        CheckColor();
    }
    
    public void SetColor(int colorIndex)
    {
        // Validate color index
        if (colorIndex < 1 || colorIndex > MAX_COLORS)
        {
            Debug.LogError($"Invalid color index: {colorIndex}. Must be between 1 and {MAX_COLORS}");
            return;
        }
        
        // Get array index (array is 0-based, but color numbers are 1-based)	
        int arrayIndex = colorIndex - 1;
        
        // Deactivate all color objects
        DeactivateAllColors();
        
        // Activate the selected color object
        colorOptions[arrayIndex].colorObject.SetActive(true);
        
        // Set the image color based on the selected color
        image.color = colorOptions[arrayIndex].imageColor;
        
        // Update the current color number
        colorNumber = colorIndex;
    }
    
    private void DeactivateAllColors()
    {
        foreach (var option in colorOptions)
        {
            option.colorObject.SetActive(false);
        }
    }
    
    // Keep the original show methods for backward compatibility
    public void ShowBlue() { SetColor(1); }
    public void ShowBlack() { SetColor(2); }
    public void ShowPink() { SetColor(3); }
    public void ShowWhite() { SetColor(4); }
    public void ShowYellow() { SetColor(5); }
}