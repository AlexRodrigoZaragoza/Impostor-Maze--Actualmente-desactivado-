<?php

	require 'ConnectionSettings.php';

	//Variables submited by user
	$nick = $_POST["nick"];
	$password = $_POST["password"];


	if($conn) {
		//Preventing sql injections
		$statement = $conn->prepare("SELECT * FROM table_user WHERE NICK = ?");
		$statement->bind_param("s",$nick);
		$statement->execute();

		$result = $statement->get_result();

		$statement->close();

		if($result->num_rows > 0){
			echo " Username is already taken";
			
		}else{
			$statement2 = $conn->prepare("INSERT INTO table_user(NICK,PASSWORD,CURRENT_POINTS,GLOBAL_POINTS) VALUES(?,?,?,?)");
			$globalPoints = 0;
			$currentPoints = 0;
			$statement2->bind_param('ssii',$nick,$password,$currentPoints,$globalPoints);

			if($statement2->execute()){
				$result = $statement2->get_result();			
				if($result->num_rows > 0){
					$data = array();
					while($obj = $result->fetch_object()){
						$data[]=$obj;
					}
					echo json_encode($data);
				}
			}else{
				echo "Error: <br>" . $conn->error;
			}
			$statement2->close();
		}
		
		$conn->close();
	}
?>