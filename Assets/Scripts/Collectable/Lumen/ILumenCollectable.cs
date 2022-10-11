public interface ILumenCollectable : ICollectable
{
    float Lumen { get; set; }

    float Collect();
    void Regen();
}

