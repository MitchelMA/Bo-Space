using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStageOption : MonoBehaviour
{
    [SerializeField] private string stageName;
    [SerializeField] private string dataHoldTag;

    private GameObject _dataObject;
    private OptionData _optionData;
    // Start is called before the first frame update
    void Start()
    {
        _dataObject = GameObject.FindWithTag(dataHoldTag);
        _optionData = _dataObject.GetComponent<OptionData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        _optionData.StageName = stageName;
    }
}
