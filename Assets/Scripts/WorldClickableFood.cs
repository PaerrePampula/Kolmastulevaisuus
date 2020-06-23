using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class WorldClickableFood : FoodPreparer
{
    public override void PrepareFood()
    {
        base.PrepareFood();
        if (PlayerDataHolder.Current.PlayerMoney.getValue<float>() >= cost)
        {
            Destroy(gameObject);
        }
    }
}

