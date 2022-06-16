using UnityEngine;

/// <summary>
/// An option of a player
/// this class consists of 3 variables <br/>
///   - Character: Character is of type `GameObject` and contains the prefab of the character <br/>
///   - Name: The name of the character<br/>
///   - Empty: Empty dictates if the option was reverted back to `empty`. Empty could be used to check if an option
/// isn't chosen yet. When it gets set to `Empty = true`, know that this cannot be reverted, and thus a new instance
/// should be initialized if you want it be set back to `Empty = false`
/// </summary>
public struct CharOption
{
    // prefab of the character
    private readonly GameObject _character;
    // name of the character
    private readonly string _name;
    // is this option empty or not
    private bool _empty;

    /// <summary>
    /// Prefab of the chosen character
    /// </summary>
    public GameObject Character => _character;
    /// <summary>
    /// Name of the chosen character
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Status of the chosen character
    /// </summary>
    public bool Empty => _empty;

    /// <summary>
    /// Constructor for an option
    /// </summary>
    /// <param name="character">The prefab of the character option</param>
    /// <param name="name">The name of the character</param>
    public CharOption(GameObject character, string name)
    {
        this._character =  character;
        this._name = name;
        _empty = false;
    }

    /// <summary>
    /// Constructor to copy another Option
    /// </summary>
    /// <param name="copy">The option that gets copied</param>
    public CharOption(CharOption copy)
    {
        this._character = Object.Instantiate(copy._character);
        this._name = copy._name;
        this._empty = false;
    }

    /// <summary>
    /// Public method to set the option itself as *empty*
    /// </summary>
    public void SetEmpty()
    {
        _empty = true;
    }
    
    public static CharOption EmptyCon() {
        var tmp = new CharOption(null, "empty-container");
        tmp.SetEmpty();
        return tmp;
    }
}