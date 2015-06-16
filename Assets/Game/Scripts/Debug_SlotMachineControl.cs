using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Debug_SlotMachineControl : MonoBehaviour {

    public SlotMachine slotmachine;

    Dictionary<int, string> table_symbolname;
    
    void Start()
    {
        table_symbolname = new Dictionary<int, string>();
        table_symbolname.Add(1, "Apple");
        table_symbolname.Add(2, "Bell");
        table_symbolname.Add(3, "Orange");
        table_symbolname.Add(4, "strawberry");
        table_symbolname.Add(5, "Vine");
    }
    public void OnClick_Spin()
    {
        slotmachine.StartSpin();
    }
    public void OnClick_Stop()
    {
        slotmachine.OnClick_Stop();
    }
    public void OnClick_StartStop()
    {
        slotmachine.OnClick_StartStop();
    }
    public void OnClick_SetSpriteInfo()
    {
        string[] info = new string[15];

        for(int i = 0; i < 15; i++)
            info[i] = table_symbolname[Random.Range(1, 5)];

        slotmachine.SetTileSpriteInfo(info);
    }
}
