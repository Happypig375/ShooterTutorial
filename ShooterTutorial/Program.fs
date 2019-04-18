open System

let player_y, enemy_y = 20, 0
let rec loop (player_x, enemy_x, enemy_direction, enemy_lives, bullets) =
    Console.Clear()
    Console.SetCursorPosition (player_x, player_y)
    Console.Write 'O'
    Console.SetCursorPosition (enemy_x, enemy_y)
    Console.Write (enemy_lives.ToString())

    //Implement life deduction
    for bullet_x, bullet_y in bullets do
        Console.SetCursorPosition (bullet_x, bullet_y)
        Console.Write '|'

    let key = Console.ReadKey()

    let new_bullets =
        [for bullet_x, bullet_y in bullets do
            match bullet_y with
            | 0 -> ()
            | _ -> yield bullet_x, bullet_y - 1
         match key.Modifiers.HasFlag(ConsoleModifiers.Control) with
         | true -> yield player_x, player_y - 1
         | false -> ()]
    
    let new_enemy_x, new_enemy_direction =
        match enemy_x, enemy_direction with
        | 0, -1 -> 1, 1
        | 30, 1 -> 29, -1
        | _, _ -> enemy_x + enemy_direction, enemy_direction

    let new_player_x =
        match player_x, key.Key with
        | 0, ConsoleKey.LeftArrow -> 0
        | 30, ConsoleKey.RightArrow -> 30
        | _, ConsoleKey.LeftArrow -> player_x - 1
        | _, ConsoleKey.RightArrow -> player_x + 1
        | _, _ -> player_x

    loop (new_player_x, new_enemy_x, new_enemy_direction, enemy_lives, new_bullets)

loop (0, 0, 1, 9, [])