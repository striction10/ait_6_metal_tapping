import './App.css'
import { Routes, Route} from "react-router-dom"
import { UserProvider } from './contexts/UserContext'
import Auth from "./pages/Auth/Auth"
import Parametres from "./pages/Parametres/Parametres"
import Reglaments from "./pages/Reglaments/Reglaments"
import Tasks from "./pages/Tasks/Tasks"

function App() {
  return (
    <UserProvider>
      <Routes>
        <Route path="/" element={<Auth />} />
        <Route path="/auth" element={<Auth />} />
        <Route path="/parametres" element={<Parametres />} />
        <Route path="/reglaments" element={<Reglaments />} />
        <Route path="/tasks" element={<Tasks />} />
      </Routes>
    </UserProvider>
  )
}

export default App;