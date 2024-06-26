﻿using System;

namespace AbstractMedia.Core.Services;

/// <summary>
///     You would need to inject your interfaces here to execute the methods in Invoke()
///     See the commented out code as an example
/// </summary>

// This is the use the file instead of DB
//public class MainService : IMainService
//{
//    private readonly IMediaService _mediaService;

//    public MainService(IMediaService mediaService)
//    {
//        _mediaService = mediaService;
//    }

//    public void Invoke()
//    {
//        var exit = false;
//        while (!exit)
//        {
//            Console.WriteLine("Welcome to the Media Library!");
//            Console.WriteLine("Choose an option:");
//            Console.WriteLine("1. Enter new media");
//            Console.WriteLine("2. Find media");
//            Console.WriteLine("3. List all media of a specific type");
//            Console.WriteLine("4. Search media by title");
//            Console.WriteLine("5. Exit");
//            Console.Write("Enter your choice (1, 2, 3, 4, or 5): ");
//            var choice = Console.ReadLine();

//            switch (choice)
//            {
//                case "1":
//                    _mediaService.EnterNewMedia();
//                    break;
//                case "2":
//                    _mediaService.FindMedia();
//                    break;
//                case "3":
//                    _mediaService.ListMediaByType();
//                    break;
//                case "4":
//                    _mediaService.SearchMedia();
//                    break;
//                case "5":
//                    exit = true;
//                    break;
//                default:
//                    Console.WriteLine("Invalid choice. Please enter 1, 2, 3, or 4.");
//                    break;
//            }
//        }
//    }
//}
public class MainService : IMainService
{
    private readonly IMediaService _mediaService;

    public MainService(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public void Invoke()
    {
        var exit = false;
        while (!exit)
        {
            Console.WriteLine("Welcome to the Movie Library!");
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1. Enter new Moive: ");
            Console.WriteLine("2. Search Movie: ");
            Console.WriteLine("3. List all Movies: ");
            Console.WriteLine("4. Update Movie: ");
            Console.WriteLine("5. Delete Movie: ");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1, 2, 3, 4, 5, or 6): ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //_mediaService.EnterNewMedia();
                    break;
                case "2":
                    //_mediaService.FindMedia();
                    break;
                case "3":
                    //_mediaService.ListMediaByType();
                    break;
                case "4":
                    //_mediaService.SearchMedia();
                    break;
                case "5":
                    //_mediaService.SearchMedia();
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, 3, 4, 5, or 6.");
                    break;
            }
        }
    }
}