namespace FunctionalDelivery.Domain.Services

open System
open FunctionalDelivery.Domain.Model

module DroneService =
    type DroneService =
        abstract DeliverOrder : id: int -> commandList : string[] -> string[]

    type DefaultDroneService(blockLimit : int) =
        interface DroneService with
            member _.DeliverOrder id commandList =

                let drone = Drone.Create(id)

                let FoldRoute(d : Drone, r : string) =
                    let md = d.Move(r)
                    if (md.Position.IsOutOfRange blockLimit) then //Do not move!
                        //(d.Position.ToString(), d)
                        raise (ArgumentException(String.Format("Can not move more than {0} blocks!", blockLimit)))
                    else
                        (md.Position.ToString(), md)

                let (destinations, _) = Array.mapFold<string, Drone, string>  (fun p r -> FoldRoute(p, r)) drone commandList
                destinations