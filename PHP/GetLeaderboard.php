<?php

	require 'ConnectionSettings.php';

	if($conn) {
		//Preventing sql injections
		$statement = $conn->prepare("SELECT NICK,GLOBAL_POINTS FROM table_user ORDER BY GLOBAL_POINTS DESC");

		$statement->execute();

		$result = $statement->get_result();

		$statement->close();
		
		if($result->num_rows > 0){
			$data = array();
			while($obj = $result->fetch_object()){
				$data[]=$obj;
			}
			echo json_encode($data);
		}else{
			echo "There isn't users registered yet";
		}
		
		$conn->close();
	}
?>