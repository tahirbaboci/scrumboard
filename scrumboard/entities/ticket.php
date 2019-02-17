<?php
class ticket{

    // Connection instance
    private $connection;

    // table name
    private $table_name = "tickets";

    // table columns
    public $id;
    public $tickettittle;
    public $ticketdesc;
    public $creationdate;
    public $status;
    public $sprint;
    public $solution;
    public $idboard;

    public function __construct($connection){
        $this->connection = $connection;
    }

    //C
    public function create(){
      // query to insert record
      $query = "INSERT INTO tickets SET
                  tickettitle=:tickettitle, ticketdesc=:ticketdesc, sprint=:sprint, idboard=:idboard";

      $stmt = $this->connection->prepare($query);

      $this->tickettitle=htmlspecialchars(strip_tags($this->tickettitle));
      $this->ticketdesc=htmlspecialchars(strip_tags($this->ticketdesc));
      $this->sprint=htmlspecialchars(strip_tags($this->sprint));
      $this->idboard=htmlspecialchars(strip_tags($this->idboard));

      $stmt->bindParam(":tickettitle", $this->tickettitle);
      $stmt->bindParam(":ticketdesc", $this->ticketdesc);
      $stmt->bindParam(":sprint", $this->sprint);
      $stmt->bindParam(":idboard", $this->idboard);

      if($stmt->execute()){
          return true;
      }
      return false;
    }

    //R
    public function read($sprint, $idBoard){
        $query = "SELECT * FROM tickets WHERE sprint=:sprint AND idBoard=:idBoard";
        $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
        $stmt->execute(array(':sprint' => $sprint, ':idBoard' => $idBoard));
        return $stmt;
    }
    public function readOne($id){
        $query = "SELECT * FROM tickets WHERE id=:id";
        $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
        $stmt->execute(array(':id' => $id));
        return $stmt;
    }
    public function readStatus($id){
      $query = "SELECT status FROM tickets WHERE id = :id";
      $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
      $stmt->execute(array(':id' => $id));
      $stmt->execute();
      return $stmt;
    }
    public function readSprintInfo(){
      $query = "SELECT sprint FROM tickets GROUP BY sprint";
      $stmt = $this->connection->prepare($query);
      $stmt->execute();
      return $stmt;
    }
    //U
    public function updateStatus($id, $solution){
      $stmt = $this->readStatus($id);

      if($stmt->rowCount() > 0){
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        if($row['status'] == 0){
          $query = "UPDATE tickets SET status = 1, solution=:solution WHERE id=:id";
          $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
          $stmt->execute(array(':id' => $id, ':solution' => $solution));
          return $stmt;
        }else{
          return false;
        }
      }
      else {
        return "notExisting";
      }
    }
    public function checkForUpdate(){
      $query = "SELECT * FROM tickets";
      $stmt = $this->connection->prepare($query);
      $stmt->execute();
      return $stmt;
    }
    //D
    public function delete($id){
      $query = "DELETE FROM tickets WHERE id=:id";
      $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
      $stmt->execute(array(':id' => $id));

      if($stmt->rowCount() > 0){
          return true;
      }
      return false;
    }
}
?>
