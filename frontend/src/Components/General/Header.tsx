/** @jsxImportSource @emotion/react */
import { Link } from "react-router-dom";
import {
  headerContainer,
  searchInput,
  signInAnchor,
  titleAnchor,
} from "../../Styles/General/HeaderStyles";
import { UserIcon } from "./Icons";

export const Header = () => {
  return (
    <div css={headerContainer}>
      <Link to="/" css={titleAnchor}>
        Cure-All
      </Link>
      <input type="text" placeholder="Search for doctor..." css={searchInput} />
      <Link to="signin" css={signInAnchor}>
        <UserIcon />
        <span>Sign In</span>
      </Link>
    </div>
  );
};
