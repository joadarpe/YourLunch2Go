namespace FunctionalDelivery.Infraestructure.Services

open System
open System.IO
open FunctionalDelivery.Domain.Services.DroneService

module DroneDeliveryService =
    type DroneDeliveryService =
        abstract DeliverOrders : inPath : string -> unit

    type DefaultDroneDeliveryService(droneLimit : int, blockLimit : int) =
        interface DroneDeliveryService with
            member _.DeliverOrders inPath =

                let CreateDestinationName(f : FileInfo) = f.FullName.Replace(f.Name, f.Name.Replace("in", "out"))
                let droneService = DefaultDroneService(blockLimit) :> DroneService

                let DeliverOrder(f : FileInfo) =
                    let droneId =
                        match Int32.TryParse (f.Name.Substring(2, 2)) with
                        | true,int -> int
                        | _ -> raise (ArgumentException("Incorrect file name format!"))
                    
                    let pos = droneService.DeliverOrder droneId (File.ReadAllLines(f.FullName))
                    let info = Array.append [|"== Delivery report =="|] pos
                    File.WriteAllLines(CreateDestinationName(f), info)

                let files = Directory.GetFiles(inPath, "in*.txt") |> Array.sort |> Array.map FileInfo
                let limit = (min files.Length droneLimit)
                files |> Array.take limit |> Array.iter DeliverOrder