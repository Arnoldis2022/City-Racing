using System.Collections.Generic;
using UnityEngine;

public class BodyPainterWindow : PainterWindow
{
    [SerializeField] private Player _player;
    [SerializeField] private float _paintingPrice;

    public override void OnEnable()
    {
        base.OnEnable();
        var playerCar = _player.Car;
        var paintedDetails = new List<PaintedDetail>() { playerCar.Body };

        if (playerCar.SpoilerPlace.Spoiler != null)
            paintedDetails.Add(playerCar.SpoilerPlace.Spoiler);

        Painter.Init(paintedDetails, _paintingPrice);
    }
}
