<?php
class task{

    // Connection instance
    private $connection;

    // table columns
    public $id;
    public $tasktitle;
    //public $creationdate;
    public $idcategory;
    public $idticket;
    public $iddeveloper;

    public function __construct($connection){
        $this->connection = $connection;
    }

    //C
    public function create(){
      // query to insert record
      $query = "INSERT INTO tasks SET
                  tasktitle=:tasktitle, idcategory=:idcategory, idticket=:idticket";

      $stmt = $this->connection->prepare($query);

      $this->tasktitle=htmlspecialchars(strip_tags($this->tasktitle));
      $this->idcategory=htmlspecialchars(strip_tags($this->idcategory));
      $this->idticket=htmlspecialchars(strip_tags($this->idticket));

      $stmt->bindParam(":tasktitle", $this->tasktitle);
      $stmt->bindParam(":idcategory", $this->idcategory);
      $stmt->bindParam(":idticket", $this->idticket);

      if($stmt->execute()){
          return true;
      }
      return false;
    }
    //R
    public function read(){
        $query = "SELECT t.id, t.tasktitle, t.creationdate, t.idCategory, t.idTicket, t.idDeveloper, tc.category, tic.tickettitle FROM tasks t
                                                                                  INNER JOIN taskscategories tc on t.idCategory = tc.id
                                                                                  INNER JOIN tickets tic on t.idTicket = tic.id";
        $stmt = $this->connection->prepare($query);
        $stmt->execute();
        return $stmt;
    }
    public function readOne($id){
        $query = "SELECT * FROM tasks WHERE id=:id";
        $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
        $stmt->execute(array(':id' => $id));
        return $stmt;
    }
    public function FindTaskOwner($id){
        $query = "SELECT d.shortname, d.firstname, d.lastname FROM tasks t
                    INNER JOIN developers d on t.idDeveloper = d.id
                    WHERE t.id=:id";
        $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
        $stmt->execute(array(':id' => $id));
        return $stmt;
    }
    public function readCategory($id){
      $query = "SELECT t.idcategory, tc.category FROM tasks t INNER JOIN taskscategories tc ON t.idcategory = tc.id WHERE t.id = :id";
      $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
      $stmt->execute(array(':id' => $id));
      $stmt->execute();
      return $stmt;
    }
    public function readTasksByTicketId($ticketId){
      $query = "SELECT * FROM tasks t WHERE t.idTicket=:idticket";
      $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
      $stmt->execute(array(':idticket' => $ticketId));
      $stmt->execute();
      return $stmt;
    }
    //U
    public function updateCategory($id){
      $stmt = $this->readCategory($id);

      if($stmt->rowCount() > 0){
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        if($row['idcategory'] == 7){
          $query = "UPDATE tasks SET idcategory = 8 WHERE id=:id";
          $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
          $stmt->execute(array(':id' => $id));
          return $stmt;
        }else if($row['idcategory'] == 8){
          $query = "UPDATE tasks SET idcategory = 9 WHERE id=:id";
          $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
          $stmt->execute(array(':id' => $id));
          return $stmt;
        }
        else{
          return false;
        }
      }
      else {
        return "notExisting";
      }
    }
    public function updateTaskToDeveloper($id, $idDeveloper){
      $stmt = $this->readOne($id);

      if($stmt->rowCount() > 0){
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        if($row['idDeveloper'] == null){
          $query = "UPDATE tasks SET idDeveloper=:idDeveloper WHERE id=:id";
          $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
          $stmt->execute(array(':id' => $id, ':idDeveloper' => $idDeveloper));
          return $stmt;
        }
        else{
          return false;
        }
      }
      else {
        return "notExisting";
      }
    }
    //D
    public function delete($id){
      $query = "DELETE FROM tasks WHERE id=:id";
      $this->id=htmlspecialchars(strip_tags($this->id));
      $stmt = $this->connection->prepare($query, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
      $stmt->execute(array(':id' => $id));

      if($stmt->rowCount() > 0){
          return true;
      }
      return false;
    }

}
?>
