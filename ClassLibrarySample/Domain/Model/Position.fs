namespace FunctionalDelivery.Domain.Model

open System

type Position = class
    val XAxis : int
    val YAxis : int
    val Direction : char

    private new () =
        { XAxis = 0; YAxis = 0; Direction = 'N' }

    new (x, y, d) =
        { XAxis = x; YAxis = y; Direction = d }

    static member Default() =
        Position()

    member p.IsOutOfRange(limit: int) =
        (abs p.XAxis) > limit || (abs p.YAxis) > limit

    override p.ToString() =
        let direction =
            match p.Direction with
            | 'N' -> "North"
            | 'E' -> "East"
            | 'S' -> "South"
            | 'W' -> "West"
            | _ -> raise (ArgumentException(String.Format("Unknown direcction {0}", p.Direction)))
        String.Format("({0}, {1}) Ahead {2}", p.XAxis, p.YAxis, direction)
end