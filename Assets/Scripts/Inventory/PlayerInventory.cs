using UnityEngine;

namespace Inventory
{
    public class PlayerInventory : AbstractInventory
    {
        [SerializeField] private ItemData _testConfig;

        //Hack: Debug only, Delete it later
        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                foreach (var item in Storage.Items)
                    Debug.Log($"{item.ItemData.Name} - {item.Amount}");

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Storage.Add(_testConfig, 5);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Storage.TryRemove(_testConfig, 7) == false)
                    Debug.Log("Not Enough");
            }

        }
    }
}
