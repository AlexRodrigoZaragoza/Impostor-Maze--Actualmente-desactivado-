<?php

require 'ConnectionSettings.php';

//Variables submited by user
$message= $_POST["message"];
$user = $_POST["user"];
$position = $_POST["position"];
$idMaze = $_POST["idMaze"];

	if(!$conn) {
	echo "la conexión no estaría funcionando<br/>";
	die("No pudo conectarse: " . $conn->connect_error);

	}else{
		//Preventing sql injections
			$statement = $conn->prepare("INSERT INTO table_message(MESSAGE,USER,POSITION,MAZE) VALUES(?,?,?,?)");

			$statement->bind_param('sisi',$message, $user, $position, $idMaze);

			if($statement->execute()){
				//Selecciona la ultima fila creada
				$statement2 = $conn->prepare("SELECT ID,MESSAGE,USER,POSITION,MAZE FROM table_message WHERE MAZE = ? ORDER BY ID DESC LIMIT 1");
				$statement2->bind_param("i",$idMaze);
				$statement2->execute();

				$result = $statement2->get_result();

				$statement2->close();
				
				if($result->num_rows > 0){
					$data = array();
					while($obj = $result->fetch_object()){
						$data[]=$obj;
					}
					echo json_encode($data);
				}
			}else{
				echo "Error: " .$statement . "<br>" . $conn->error;
			}
			$statement->close();
			$conn->close();		
	}
?>