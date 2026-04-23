import { useEffect, useState } from "react";
import { getUserFromToken } from "../../utils/authUser";
import ManagerDashboard from "./ManagerDashboard";
import UserDashboard from "./UserDashboard";

export default function Dashboard() {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const currentUser = getUserFromToken();
    setUser(currentUser);
  }, []);

  if (!user) return <p>Loading...</p>;

  const isManager =
    user.role === "Administrator" || user.role === "Manager";

  return (
    <div>
      {isManager ? <ManagerDashboard /> : <UserDashboard />}
    </div>
  );
}