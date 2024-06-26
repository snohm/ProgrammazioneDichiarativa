type  'a fbtree = Node of 'a * (('a fbtree) list)

let tree1 
  = Node(0,
         [ Node(1, 
               [ Node(2,[]) ;
                 Node(3,[]) ;
                 Node(4,[]) ;
                 Node(5,[]) 
               ]) ;
           Node(6,[]) ;
           Node(7,
               [
                Node(8, [Node(9,[])]) ;
                Node(10,[])  
               ])
          ])  


let tree2 = 
     Node(0,
          [ Node(1, [ Node(2,[]) ; Node(3,[]) ]) ; 
            Node(4, [ Node(5,[]) ; Node(6,[]) ; Node(7,[]) ]) ;    
            Node(8, [])
          ])      
      

let tree3 = 
     Node(0,
          [ Node(1, [Node(2,[])]) ;
            Node(3,
               [ Node(4, [Node(5,[]); Node(6,[])]) ;
                 Node(7,[]) ;
                 Node(8,[Node(10, [])]) ;
                 Node(9,[]) ;

               ])      
          ])

let chadFbtree = Node(0, 
                      [Node(1, [Node(4, [
                                         Node(5, [])
                                         Node(6, [Node(9, []) 
                                                  Node(10, [])])
                                         Node(7, [])
                                         Node(8, [Node(11, [])])
                                        ])])
                       Node(2, [
                                Node(12, [])
                                Node(13, [])
                                Node(14, [
                                          Node(15, [])
                                          Node(16, [
                                                    Node(17, [
                                                              Node(18, [])
                                                              Node(19, [])
                                                              ])
                                                    ])
                                              ])
                                  ]) 
                       Node(3, [Node(20, [Node(21, [
                                                     Node(22, [Node(23, [
                                                                        Node(24, [Node(26, [])])
                                                                        Node(25, [])
                                                                        Node(27, [])
                                                                        ])])
                                                     Node(28, [])     
                                                   ])])])                       
                      ])
// ***************************************************************************************************************************//
//                    Le funzioni dovrebbero essere definite su una liena mediante funzioni HO ma non sono capace             //
// ***************************************************************************************************************************//
// 1) count : 'a fbtree -> int
//che conta i nodi di un albero.
let rec count fbtree = 
    let rec countI ys =
      let y = List.head ys
      match (y,ys) with
      //| Node(_, []), [] -> 1 // can't happen
      | Node(_, []), _::[] -> 1 
      | Node(_, []), _::xs -> 1 + countI xs 
      
      //| Node(_, y::[]), [] -> 1 + count y // can't happen
      | Node(_, y::[]), _::[] -> 1 + count y
      | Node(_, y::[]), _::xs -> 1 + count y + countI xs  

      | Node(_, y::ys), _::[] -> 1 + count y + countI ys  
      | Node(_, y::ys), _::xs -> 1 + count y + countI ys  + countI xs  

    match fbtree with 
    | Node(_, []) -> 1
    | Node(_, y::[]) -> 1 + count y
    | Node(_, y::ys) -> 1 + count y + countI ys 

let rec contains fbtree x = 
    let rec countI ys x =
      let y = List.head ys
      match (y,ys) with
      | Node(n, []), _::[] -> n = x 
      | Node(n, []), _::xs -> if n <> x then countI xs x else true 

      | Node(n, y::[]), _::[] -> if n <> x then contains y x else true
      | Node(n, y::[]), _::xs -> if n <> x then  contains y x || countI xs x else true  

      | Node(n, y::ys), _::[] -> if n <> x then  contains y x || countI ys x else true 
      | Node(n, y::ys), _::xs -> if n <> x then  contains y x || countI ys x || countI xs x else true  
    match (fbtree, x) with 
    | Node(n, []), x -> n = x  
    | Node(n, y::[]), x ->  if n <> x then contains y x else true
    | Node(n, y::ys), x ->  if n <> x then contains y x || countI ys x else true  
  
let rec tree2list fbtree =
    let rec countI ys =
      let y = List.head ys
      match (y,ys) with
      | Node(x, []), _::[] -> [x] 
      | Node(x, []), _::xs -> [x] @ countI xs 
      
      | Node(x, y::[]), _::[] -> [x] @ tree2list y
      | Node(x, y::[]), _::xs -> [x] @ tree2list y @ countI xs  

      | Node(x, y::ys), _::[] -> [x] @ tree2list y @ countI ys  
      | Node(x, y::ys), _::xs -> [x] @ tree2list y @ countI ys @ countI xs  

    match fbtree with 
    | Node(x, []) -> [x]
    | Node(x, y::[]) -> [x] @ tree2list y
    | Node(x, y::ys) -> [x] @ tree2list y @ countI ys 