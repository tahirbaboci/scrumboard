   <?php
   header("Content-Type: application/json; charset=UTF-8");

   include_once '../config/dbclass.php';
   include_once '../entities/ticket.php';

   $dbclass = new DBClass();
   $connection = $dbclass->getConnection();

   $ticket = new ticket($connection);

   //$id = $_POST["id"];
   $id = 2;

   $stmt = $ticket->checkForUpdate();
   $count = $stmt->rowCount();

   if($count > 0){
     echo json_encode($count);
   }
   else {
     echo '[]';
   }
   ?>
