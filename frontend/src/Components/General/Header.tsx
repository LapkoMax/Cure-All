/** @jsxImportSource @emotion/react */
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
      <a href="./" css={titleAnchor}>
        Cure-All
      </a>
      <input type="text" placeholder="Search for doctor..." css={searchInput} />
      <a href="./signin" css={signInAnchor}>
        <UserIcon />
        <span>Sign In</span>
      </a>
    </div>
  );
};
