module Features.DeliverOrdersByDrone

open Xunit
open System.IO
open FunctionalDelivery.Infraestructure.Services.DroneDeliveryService
open System


let BuildExpectedFullName (fI : FileInfo) =
    fI.FullName.Replace(fI.Name, "expected-"+fI.Name)

let AssertFileContent (fI : FileInfo) =
    Assert.Equal<string[]>(File.ReadAllLines(BuildExpectedFullName(fI)), File.ReadAllLines(fI.FullName))
    fI.FullName


[<Fact>]
let ``Should create one output file with drone position repport for every input file`` () =

    // Given one group of drone input files
    let workingPath = Path.Combine(".", "Features", "Resources", "ValidInput")

    // When Drone service is executed
    let service = new DefaultDroneDeliveryService(20, 10) :> DroneDeliveryService
    service.DeliverOrders workingPath

    // Then the output files must be equals at the expected output files
    let expectedfilesInfo = Directory.GetFiles(workingPath, "expected-out*.txt") |> Array.sort |> Array.map FileInfo
    let outFiles = Directory.GetFiles(workingPath, "out*.txt") |> Array.sort |> Array.map FileInfo

    Assert.NotNull(outFiles);
    Assert.Equal(expectedfilesInfo.Length, outFiles.Length)

    // And the content should be the expected
    outFiles |> Array.iter (AssertFileContent >> File.Delete)

[<Fact>]
let ``Should process no more than N drones at a time`` () =

    // Given one group of drone input files
    let workingPath = Path.Combine(".", "Features", "Resources")
    // And a limit of drones at a time
    let droneLimit = 1

    // When Drone service is executed
    let service = new DefaultDroneDeliveryService(droneLimit, 10) :> DroneDeliveryService
    service.DeliverOrders workingPath

    // Then the output files must be no more than the droneLimit
    let outFiles = Directory.GetFiles(workingPath, "out*.txt") |> Array.map FileInfo
    Assert.NotNull(outFiles);
    Assert.Equal(droneLimit, outFiles.Length)

    // And the content should be de expected
    outFiles |> Array.iter (AssertFileContent >> File.Delete)

[<Fact>]
let ``Should not deliver more than N blocks far`` () =

    // Given one group of drone input files
    let workingPath = Path.Combine(".", "Features", "Resources")
    // And a limit of 10 blocks for deliver
    let blockLimit = 10

    // When Drone service is executed
    let service = new DefaultDroneDeliveryService(20, blockLimit) :> DroneDeliveryService
    let shouldThrow = fun() -> service.DeliverOrders workingPath

    // Then the validation should be thrown
    Assert.Throws<ArgumentException>(shouldThrow)
    