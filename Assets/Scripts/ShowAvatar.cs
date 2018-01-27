using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.UI;

public class ShowAvatar : MonoBehaviour {

    public AvatarConfig config;
    public string title;
    private Image background;
    private Text word;

    // Use this for initialization
    void Start ()
    {
        background = this.gameObject.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>();
        word = this.gameObject.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>();

        GenerateAvatar();
    }
	
    public void GenerateAvatar()
    {
        byte[] hash = MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(title));
        int hashed = Mathf.Abs(System.BitConverter.ToInt32(hash, 0));

        background.color = config.colors[hashed % config.colors.Length];
        word.font = config.fonts[hashed % config.fonts.Length];
        if( title.Length <= 0)
        {
            title = "*";
        }
        string firstLetter = "" + title.ToCharArray()[0];
        if ( hashed % 2 == 0 )
        {
            firstLetter = ToggleCase(firstLetter);
        }
        word.text = firstLetter;
    }


    private string ToggleCase(string input)
    {
        string result = string.Empty;
        char[] inputArray = input.ToCharArray();

        foreach (char c in inputArray)
        {
            if (char.IsLower(c))
                result += c.ToString().ToUpper();
            else if (char.IsUpper(c))
                result += c.ToString().ToLower();
            else
                result += c.ToString();
        }

        return result;
    }
}
