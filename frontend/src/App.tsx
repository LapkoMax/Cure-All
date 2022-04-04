/** @jsxImportSource @emotion/react */
import { appContainer } from "./AppStyles";
import { Header } from "./Components/General/Header";
import { HomePage } from "./Components/HomePage";

function App() {
  return (
    <div css={appContainer}>
      <Header />
      <HomePage />
    </div>
  );
}

export default App;
