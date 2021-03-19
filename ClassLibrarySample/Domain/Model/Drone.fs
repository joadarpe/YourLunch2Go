namespace FunctionalDelivery.Domain.Model

type Drone = class
    val Id: int
    val Position: Position

    private new (id) =
        { Id = id; Position = Position.Default() }

    private new (id, position) =
        { Id = id; Position = position }

    static member Create(id) =
        Drone(id)

    static member Create(id: int, position: Position) =
        Drone(id, position)

    member d.Move(r: string) =
        let commands = r.ToCharArray()
        Array.fold<char, Drone> (fun d c -> d.Move(c)) d commands

    member private d.Move(c: char) =
        match c with
        | 'A' -> d.GoAhead()
        | 'D' -> d.TurnRigth()
        | 'I' -> d.TurnLeft()
        | _ -> d

    member private d.GoAhead() =
        let p = d.Position
        match p.Direction with
        | 'N' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis + 1, p.Direction))
        | 'E' -> Drone.Create(d.Id, Position(p.XAxis + 1, p.YAxis, p.Direction))
        | 'S' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis - 1, p.Direction))
        | 'W' -> Drone.Create(d.Id, Position(p.XAxis - 1, p.YAxis, p.Direction))
        | _ -> d

    member private d.TurnRigth() =
        let p = d.Position
        match p.Direction with
        | 'N' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis, 'E'))
        | 'E' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis, 'S'))
        | 'S' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis, 'W'))
        | 'W' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis, 'N'))
        | _ -> d

    member private d.TurnLeft() =
        let p = d.Position
        match p.Direction with
        | 'N' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis, 'W'))
        | 'E' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis, 'N'))
        | 'S' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis, 'E'))
        | 'W' -> Drone.Create(d.Id, Position(p.XAxis, p.YAxis, 'S'))
        | _ -> d

end