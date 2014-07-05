using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CandleconfigurationHelper {

    //returns normalised[-1.0f, 1.0f] space candle positions relative to center of ritual space i.e. main symbol position
    public static List<Vector2> GetCandlePositions(CandleConfigurations.CandleConfig config)
    {
        List<Vector2> candlePositionList = new List<Vector2>();
        switch (config)
        {
            case CandleConfigurations.CandleConfig.TRIANGLE:
                {
                    //top
                    candlePositionList.Add(new Vector2(0.0f, 1.0f));
                    //right
                    candlePositionList.Add(new Vector2(1.0f, -1.0f));
                    //left
                    candlePositionList.Add(new Vector2(-1.0f, -1.0f));
                }
                break;

            case CandleConfigurations.CandleConfig.SQUARE:
                {
                    //top left
                    candlePositionList.Add(new Vector2(-1.0f, 1.0f));
                    //top right
                    candlePositionList.Add(new Vector2(1.0f, 1.0f));
                    //bottom left
                    candlePositionList.Add(new Vector2(-1.0f, -1.0f));
                    //bottom right
                    candlePositionList.Add(new Vector2(1.0f, -1.0f));
                }
                break;

            case CandleConfigurations.CandleConfig.PENTAGRAM:
                {
                    //top
                    candlePositionList.Add(new Vector2(0.0f, 1.0f));
                    //right
                    candlePositionList.Add(new Vector2(1.0f, 0.3f));
                    //right down
                    candlePositionList.Add(new Vector2(0.5f, -1.0f));
                    //left down
                    candlePositionList.Add(new Vector2(-0.5f, -1.0f));
                    //left
                    candlePositionList.Add(new Vector2(-1.0f, 0.3f));
                }
                break;
        }
        return candlePositionList;
    }

}
