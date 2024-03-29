import { Route, Routes } from "react-router-dom";
import BookList from "./BookList";
import Menu from "./Menu";
import NoMatch from "./NoMatch";
import RenderOnRole from "./Guards/RenderOnRole";
import SecretBooks from "./SecretBooks";

const BookBox = () => (
  <>
    <Menu/>
    <Routes>
      <Route exact path="/" element={<BookList/>}/>
      <Route path="/secret" element={
        <RenderOnRole roles={['admin']} showNotAllowed>
          <SecretBooks/>
        </RenderOnRole>
      }/>
      <Route path="*" element={<NoMatch/>}/>
    </Routes>
  </>
)

export default BookBox
