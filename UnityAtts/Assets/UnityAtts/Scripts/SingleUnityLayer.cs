using UnityEngine;

/// <summary>
/// TODO: This should be changed to a PropertyAttribute usable over an int field.
/// </summary>
[System.Serializable]
public class SingleUnityLayer
{
    [SerializeField]
    private int m_LayerIndex = 0;
    public int LayerIndex
    {
        get { return m_LayerIndex; }
    }

    public void Set(int _layerIndex)
    {
        if (_layerIndex > 0 && _layerIndex < 32)
        {
            m_LayerIndex = _layerIndex;
        }
    }

    public int Mask
    {
        get { return 1 << m_LayerIndex; }
    }
}
