
import './App.scss';
import Login from './login'

import AuthProvider from "./provider/authProvider";
import Routes from "./routes";
function App() {
  return (
    <AuthProvider>
      <Routes />
    </AuthProvider>
  );

}

export default App;
