using UnityEngine;


public struct CharOption
{
    private readonly GameObject _character;
    private readonly string _name;
    private bool _empty;

    public GameObject Character => _character;
    public string Name => _name;
    public bool Empty => _empty;

    public CharOption(GameObject character, string name)
    {
        this._character = character;
        this._name = name;
        _empty = false;
    }

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