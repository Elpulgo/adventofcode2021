use std::fs;

pub fn execute(){
    println!("Day1:");

    let mut input = include_str!("apa1.txt")
        .lines()
        .map(|n| n.parse::<u32>().expect("nope"))
        .collect::<Vec<_>>();

    let mut incresead = 0;
    input.iter(|i| i > input[i])

    for elem in input {
        println!("{:?}", elem);
    }
    // let input = File fs::read("Day1.txt")
    //     .into_iter()
    //     .map(|f| f.parse::<u32>)
    //     .collect();
        
    
}