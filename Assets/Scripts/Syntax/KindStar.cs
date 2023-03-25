namespace Syntax;

public class KindStar : Kind {
    private KindStar() {}

    private static KindStar? instance = null;

    public static KindStar STAR {
        get {
            if (instance == null) {
                instance = new KindStar();
            }
            return instance;
        }
    }

    public override bool Is(Object o)
    {
        return o is KindStar;
    }

    public override string ToString()
    {
        return "*";
    }
}