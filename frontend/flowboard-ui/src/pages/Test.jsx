import { useEffect, useState } from "react";
import { getTestMessage } from "../services/testService";

function Test() {
  const [message, setMessage] = useState("");

  useEffect(() => {
    getTestMessage().then(setMessage);
  }, []);

  return (
    <div>
      <h2>API Test</h2>
      <p>{message}</p>
    </div>
  );
}

export default Test;