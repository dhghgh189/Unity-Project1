public static class Define
{
    public static UpgradeInfo[] upgradeInfos =
    {
        new UpgradeInfo("데미지", 8, 10.0f, 40),
        new UpgradeInfo("체력", 4, 1.0f, 60),
        new UpgradeInfo("시간 가속", 8, 5.0f, 40)
    };
}

public class UpgradeInfo
{
    public string name;
    public int maxLevel;
    public float amount;
    public int price;

    public UpgradeInfo(string name, int maxLevel, float amount, int price)
    {
        this.name = name;
        this.maxLevel = maxLevel;
        this.amount = amount;
        this.price = price;
    }
}
